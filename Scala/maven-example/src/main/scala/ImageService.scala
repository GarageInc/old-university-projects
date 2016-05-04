import java.io.File
import java.text.SimpleDateFormat
import java.util.Calendar

import isc.annotations.{Param, ServiceMethod}
import isc.api.http.HttpService

import scala.util.Try

/**
  * Created by RinatF on 04.05.2016.
  */

class ImageService extends HttpService{

  @ServiceMethod
  def upload_photo( request:Requests.ImageRequest ): Unit ={

    val uploaded_at = System.currentTimeMillis()
    val directory = timeToStr( uploaded_at )

    val thumbnailsDir = "%s/%s/%s" format ( Config.PHOTOS_THUMBNAILS_DIR, request.id_terminal, directory );
    val photoDir = "%s/%s/%s" format ( Config.PHTOTOS_DIR, request.id_terminal, directory );

    validateDir( photoDir )

    val photoPath = "%s/%s.jpg".format( photoDir,uploaded_at )
    moveImage( request.file_path, photoPath )

    validateDir( thumbnailsDir )
    ThumbnailsFactory.createThumbnails( thumbnailsDir, photoPath, uploaded_at )
  }

  @ServiceMethod
  def upload_screenshot(request:Requests.ImageRequest): Unit ={

    val uploaded_at = System.currentTimeMillis()
    val directory = timeToStr( uploaded_at )

    val thumbnailsDir = "%s/%s/%s" format ( Config.SCREENSHOTS_THUMBNAILS_DIR, request.id_terminal, directory )
    val screenshotDir = "%s/%s/%s" format (  Config.SCREENSHOTS_DIR, request.id_terminal, directory )

    validateDir( screenshotDir )

    val screenshotPath = "%s/%s.jpg".format( screenshotDir,uploaded_at )
    moveImage( request.file_path, screenshotPath )

    validateDir( thumbnailsDir )
    ThumbnailsFactory.createThumbnails( thumbnailsDir, screenshotPath, uploaded_at )
  }



  val formatter = new SimpleDateFormat("YYYY-MM-dd_HH");

  def timeToStr( timeStamp: Long): String ={

    val calendar = Calendar.getInstance();
    calendar.setTimeInMillis(timeStamp);

    formatter.format( calendar.getTime );
  }

  def moveImage( fromFile:String, toFile:String ): Unit ={

    Try(new File( fromFile ).renameTo( new File( toFile )))
  }

  def validateDir( path:String ): Unit ={

    val dir = new File( path );

    if( !dir.exists() ){
      dir.mkdirs();
    }
  }
}

object Requests {

  class ImageRequest {
    @Param
    var id_terminal:Int = 0

    @Param
    var file_path:String = null
  }

}
