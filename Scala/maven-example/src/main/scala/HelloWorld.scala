/**
  * Created by RinatF on 27.04.2016.
*/

import org.slf4j.LoggerFactory

object HelloWorld {

  def main(args: Array[String]) {
    println("Hello, world!")

    println("Logger:")

    val logger = LoggerFactory.getLogger(HelloWorld.getClass);
    logger.info("Hello World");
  }
}
