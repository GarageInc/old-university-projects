package osago.bb.actors

import java.io.File

import akka.actor.{Actor, ActorRef}
import ch.qos.logback.core.joran.conditional.ThenOrElseActionBase
import org.slf4j.LoggerFactory
import osago.bb.Thumbnails

import scala.util.{Failure, Success}

/**
  * Created by rudogma on 05.05.16.
  */
class ThumbnailsActor extends Actor {

  val logger = LoggerFactory.getLogger("raw.converter")
  val errorsLogger = LoggerFactory.getLogger("errors.converter")

  def receive:Receive = {
    case GenerateThumbnails(input_file, width, height) => {

      try{
        logger.debug("Generating [%dx%d] for %s" format (width, height, input_file.getAbsolutePath))

        Thumbnails.generate(input_file, width, height)

        sender() ! Success()
      }catch{
        case e:Throwable => {

          errorsLogger.error(e.getMessage, e)

          sender() ! Failure(e)
        }
      }

    }
  }
}


case class GenerateThumbnails(input_file:File, width:Int, height:Int)
