import org.apache.commons.exec.{CommandLine, DefaultExecutor}

/**
  * Created by RinatF on 28.04.2016.
  */
object ThumbnailsFactory {

  def main(  args:Array[String] ){

    trace( args );

    createBy( args(0) toInt, args(1) toInt ,args(2), args(3))
  }

  def trace( params:Array[ String ] ): Unit ={
    params.foreach(println(_))
  }

  def createThumbnails(thumbnailsDir:String, filePath:String, uploaded_at:Long): Unit ={

    var width = 400;
    var height = 235;
    createBy( width, height, filePath, "%s/%sx%s_%s.jpg" format (thumbnailsDir,width,height,uploaded_at) );

    width = 130;
    height = 77;
    createBy( width, height, filePath, "%s/%sx%s_%s.jpg" format (thumbnailsDir,width,height,uploaded_at) );
  }

  def createBy( width:Int, height:Int, sourceFilePath:String, thumbFilePath:String ){

    val command = f"vipsthumbnail $sourceFilePath%s --size $width%sx$height%s -o $thumbFilePath%s"

    val executor = new DefaultExecutor();

    val exitResult = executor.execute( CommandLine.parse(command) );

    println("exit-result:")
    println(exitResult)
  }



}
