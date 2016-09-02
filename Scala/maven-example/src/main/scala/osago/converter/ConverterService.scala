package osago.converter

import java.io.File
import java.nio.file.{Files, StandardCopyOption}
import java.text.SimpleDateFormat
import java.util.Calendar

import com.typesafe.config.ConfigFactory
import isc.annotations.{Param, ServiceMethod}
import isc.api.http.HttpService
import org.slf4j.LoggerFactory
import osago.converter.ConverterService.{DocumentUploadRequest, FopStringRequest}
import ru.dogma.io.FileUtils

/**
  * Created by rudogma on 15.05.16.
  */
class ConverterService extends HttpService {

  lazy val config = ConfigFactory.load()
  lazy val storage_path =  config.getString("converter.storage_path")

  @ServiceMethod
  def fop_string_to_pdf(request: FopStringRequest): String = {

    val uploaded_at = Calendar.getInstance()
    val timeInSeconds:Int = (uploaded_at.getTimeInMillis / 1000).toInt

    val tmp_file = new File("/tmp/%d.fo" format timeInSeconds)

    FileUtils.filePutContents(tmp_file, request.fop_string)

    try{
      val new_file = new File("%s/%s.%d.pdf" format (storage_path, request.id_external, timeInSeconds))

      FopConverter.convert(tmp_file, new_file)

      "http://bb.polis24.net/fop/files/%s.%d.pdf" format (request.id_external, timeInSeconds)
    }finally {
      tmp_file.delete()
    }

  }

  @ServiceMethod
  def fop_to_pdf(request: DocumentUploadRequest): String = {

    val uploaded_at = Calendar.getInstance()

    val timeInSeconds:Int = (uploaded_at.getTimeInMillis / 1000).toInt

    val new_file = new File("%s/%s.%d.pdf" format (storage_path, request.id_external, timeInSeconds))

    FopConverter.convert(request.uploaded_file, new_file)

    "http://bb.polis24.net/fop/%s.%d.pdf" format (request.id_external, timeInSeconds)
  }


//  protected def move_uploaded_file(uploaded_file:File, id_external:Int, uploaded_at:Calendar):File = {
//
//    val dir = new File("%s/%s/%s.%d.pdf" format (storage_path, id_external, uploaded_at:)
//
//    if(!dir.exists()){
//      dir.mkdirs()
//    }
//
//    val timeInSeconds:Int = (uploaded_at.getTimeInMillis / 1000).toInt
//
//    val new_file = new File( "%s/%d.jpg" format (dir.getAbsolutePath, timeInSeconds))
//
//    //    println("Trying rename [%s .exists=%s .writable=%s] to [%s] " format (uploaded_file.getAbsolutePath, uploaded_file.exists(), uploaded_file.canWrite, new_file.getAbsolutePath))
//
//    Files.copy(uploaded_file.toPath, new_file.toPath, StandardCopyOption.REPLACE_EXISTING)
//    //    require( uploaded_file.renameTo(new_file), "Rename [%s -> %s] failed" format (uploaded_file.getAbsolutePath, new_file.getAbsolutePath))
//
//    new_file
//  }
}


object ConverterService {

  val errorsLogger = LoggerFactory.getLogger("errors.converter")

  class DocumentUploadRequest {
    @Param
    var id_external:String = null

    @Param
    var file_path:String = null

    def uploaded_file:File = new File(file_path)
  }

  class FopStringRequest {
    @Param
    var id_external:String = null

    @Param
    var fop_string:String = null
  }

}

