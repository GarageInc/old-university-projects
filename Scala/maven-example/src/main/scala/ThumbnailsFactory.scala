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

  def createBy( width:Int, height:Int, sourceFilePath:String, thumbFilePath:String ){

//    val thumbnailName = s"thumb_$widthx%s$height%s.jpg"

    val command = f"vipsthumbnail $sourceFilePath%s --size $width%sx$height%s -o $thumbFilePath%s"

    var executor = new DefaultExecutor();

    val exitResult = executor.execute( CommandLine.parse(command) );
  }

}
