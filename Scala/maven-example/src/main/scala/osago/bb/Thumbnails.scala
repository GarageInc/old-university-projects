package osago.bb

import java.io.File

import com.typesafe.config.ConfigFactory
import org.apache.commons.exec.{CommandLine, DefaultExecutor}
import scala.io.file._

/**
  * Created by RinatF on 28.04.2016.
  */
object Thumbnails {

  lazy val vips_executable = ConfigFactory.load().getString("vips.executable")

  def generate(input_file:File, width:Int, height:Int): Unit ={

    val directory = input_file.getParent

    val input_simple_name = input_file.getName.reverse.dropWhile( _ != '.').drop(1).reverse


    val output_file = "%s/%s_%dx%d.%s" format (directory, input_simple_name, width, height, input_file.extension())

    Thumbnails.generate(input_file.getAbsolutePath, output_file, width, height)
  }

//  def generate(input_file:File, output_file:String, output_width:Int, output_height:Int): Int = {
//    generate(input_file.getAbsolutePath, output_file, output_width, output_height)
//  }

  def generate(input_file:String, output_file:String, output_width:Int, output_height:Int): Int = {

    val command = s"$vips_executable $input_file --size ${output_width}x$output_height -o $output_file"

    val executor = new DefaultExecutor()

    executor.execute( CommandLine.parse(command) )
  }

}
