package osago.bb

import java.io.{FileNotFoundException, File}
import java.nio.file.{CopyOption, Files, StandardCopyOption}
import java.text.SimpleDateFormat
import java.util.{Date, Calendar}

import com.typesafe.config.ConfigFactory
import isc.annotations.{Param, ServiceMethod}
import isc.api.http.HttpService
import org.apache.commons.exec.{CommandLine, DefaultExecutor}
import osago.bb.BBService.{ScreenshotsRequest, TerminalRequest, ImageUploadRequest}
import osago.bb.actors.GenerateThumbnails
import akka.pattern.ask
import akka.util.Timeout
import org.slf4j.LoggerFactory
import osago.OsagoConverterApp
import osago.bb.api.TerminalsApi

import scala.annotation.meta.param
import scala.collection.mutable
import scala.io.file._
import scala.concurrent.duration._

class BBService extends HttpService {

  lazy val config = ConfigFactory.load()
  lazy val storage_path = config.getString("storage.path")


  val formatter = new SimpleDateFormat("yyyy-MM-dd")


  implicit val timeout: Timeout = 60 seconds

  import OsagoConverterApp.system._

  @ServiceMethod
  def upload_photo(request: ImageUploadRequest): Boolean = {

    val now = Calendar.getInstance()

    val uploaded_file = move_uploaded_file(request.uploaded_file, request.id_terminal, "photos", now)

    for{
      a <- OsagoConverterApp.THUMBNAILS_ACTOR ? GenerateThumbnails(uploaded_file, 400, 235)
      b <- OsagoConverterApp.THUMBNAILS_ACTOR ? GenerateThumbnails(uploaded_file, 130, 77)
    } {
      try {
        TerminalsApi.photo_uploaded(request.id_terminal, now)
      }catch {
        case e:Throwable => {
          BBService.errorsLogger.error(e.getMessage, e.printStackTrace())
        }
      }
    }

    true
  }

  @ServiceMethod
  def upload_tray_photo(request: ImageUploadRequest): Boolean = {

    val now = Calendar.getInstance()

    val uploaded_file = move_uploaded_file(request.uploaded_file, request.id_terminal, "photos_tray", now)

    OsagoConverterApp.THUMBNAILS_ACTOR ? GenerateThumbnails( uploaded_file, 400, 235 )

    true
  }

  @ServiceMethod
  def upload_screenshot( request: ImageUploadRequest ): Boolean ={

    val now = Calendar.getInstance()

    val uploaded_file = move_uploaded_file(request.uploaded_file, request.id_terminal, "screenshots", now)

    for{
      a <- OsagoConverterApp.THUMBNAILS_ACTOR ? GenerateThumbnails(uploaded_file, 400, 235)
      b <- OsagoConverterApp.THUMBNAILS_ACTOR ? GenerateThumbnails(uploaded_file, 130, 77)
    } {

      try{
        TerminalsApi.screenshot_uploaded(request.id_terminal, now)
      }catch {
        case e:Throwable => {
          BBService.errorsLogger.error(e.getMessage, e.printStackTrace())
        }
      }
    }

    true
  }

  @ServiceMethod
  def tray_photos_dates(request: TerminalRequest) = {

    get_images_dirs("photos_tray", request.id_terminal)
  }

  @ServiceMethod
  def screenshots_dates(request: TerminalRequest) = {

    get_images_dirs("screenshots", request.id_terminal)
  }

  @ServiceMethod
  def screenshots_by_date(request: ScreenshotsRequest) = {
    get_images_by_date( "screenshots", request.id_terminal, request.date )
  }

  @ServiceMethod
  def tray_photos_by_date(request: ScreenshotsRequest) = {
    get_images_by_date( "photos_tray", request.id_terminal, request.date )
  }

  /** * UTILS ***/

  protected def get_images_by_date(context:String, id_terminal:Int, date:String) = {

    val dir = new File("%s/%d/%s/%s/" format(storage_path, id_terminal, context, date))

    val R_NAME = """([0-9]+).jpg""".r

    require( dir.exists(), "Directory not exists. Check sended params")

    dir.list().map({
      case R_NAME(name) => name
      case _ => null
    }).filter( _ != null).sorted
  }

  protected def get_images_dirs(context:String, id_terminal:Int) = {

    val dir = new File("%s/%d/%s/" format(storage_path, id_terminal, context))

    require( dir.exists(), "Directory not exists. Check sended params")

    dir.listFiles.filter(_.isDirectory).map(_.getName).sorted
  }

  protected def move_uploaded_file(uploaded_file: File, id_terminal: Int, context: String, uploaded_at: Calendar): File = {

    val dir = new File("%s/%d/%s/%s/" format(storage_path, id_terminal, context, formatter.format(uploaded_at.getTime)))

    if (!dir.exists()) {
      dir.mkdirs()
    }

    val timeInSeconds: Int = (uploaded_at.getTimeInMillis / 1000).toInt

    val new_file = new File("%s/%d.jpg" format(dir.getAbsolutePath, timeInSeconds))

    //    println("Trying rename [%s .exists=%s .writable=%s] to [%s] " format (uploaded_file.getAbsolutePath, uploaded_file.exists(), uploaded_file.canWrite, new_file.getAbsolutePath))

    Files.copy(uploaded_file.toPath, new_file.toPath, StandardCopyOption.REPLACE_EXISTING)
    //    require( uploaded_file.renameTo(new_file), "Rename [%s -> %s] failed" format (uploaded_file.getAbsolutePath, new_file.getAbsolutePath))

    new_file
  }


}

object BBService {

  val errorsLogger = LoggerFactory.getLogger("errors.converter")

  class ImageUploadRequest {
    @Param
    var id_terminal: Int = 0

    @Param
    var file_path: String = null

    def uploaded_file: File = new File(file_path)
  }

  class TerminalRequest {

    @Param
    var id_terminal: Int = 0;
  }

  class ScreenshotsRequest {

    @Param
    var id_terminal: Int = 0;

    @Param
    var date: String = null;
  }

}

