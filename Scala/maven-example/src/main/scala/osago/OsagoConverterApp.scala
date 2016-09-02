package osago

import akka.actor.{ActorSystem, Props}
import com.typesafe.config.ConfigFactory
import osago.bb.BBServer
import osago.bb.actors.ThumbnailsActor
import osago.converter.ConverterServer


object OsagoConverterApp {

  val config = ConfigFactory.load()

  val system = ActorSystem("bb-system")
  val THUMBNAILS_ACTOR = system.actorOf(Props[ThumbnailsActor],"thumbnails")


  def main(args:Array[String]): Unit ={

    BBServer.bind(config.getString("bb.host"), config.getInt("bb.port"))

    ConverterServer.bind(config.getString("converter.host"), config.getInt("converter.port"))
  }

}



