package osago.converter

import java.io.File

import com.typesafe.config.ConfigFactory
import org.apache.commons.exec.{CommandLine, DefaultExecutor}

/**
  * Created by rudogma on 15.05.16.
  */
object FopConverter {

  lazy val config = ConfigFactory.load()

  lazy val fop_config = config.getString("fop.config")


  def convert(input:File, output:File): Unit ={

    val command = s"fop ${input} ${output} -c ${fop_config}"

    val executor = new DefaultExecutor()

    val exit_code = executor.execute( CommandLine.parse(command) )

    require( exit_code == 0, "[fop.converter] Exit code invalid(%d) " format exit_code)
  }

}
