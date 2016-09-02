package osago.converter

import java.nio.channels.ClosedChannelException

import akka.actor.ActorSystem
import io.netty.channel.Channel
import isc.FaviconException
import isc.api.http.HttpToXmlApiServer
import logging.HttpServerLogger
import ru.dogma.netty.http.{FullHttpRequest, FullHttpResponse}

/**
  * Created by rudogma on 15.05.16.
  */
object ConverterServer extends HttpToXmlApiServer {

  val api = ConverterApi

  val system = ActorSystem("converter-server")

  protected val httpLogger = new HttpServerLogger("access.converter","errors.converter")

  override def handler_exception(ch: Channel, httpRequest:FullHttpRequest): PartialFunction[Throwable, Unit] = {

    case e:FaviconException => {
      writeError(ch, 500, e.getMessage)
    }

    case e:Throwable => {
      if( !e.isInstanceOf[ ClosedChannelException ]){
        writeError(ch, 500, e.getMessage)
      }
    }
  }

  def writeError(ch:Channel, errorCode:Int, errorMessage:String): Unit ={

    val response = <x status={errorCode.toString} msg={errorMessage} />

    val httpResponse = new FullHttpResponse().replaceContent(response.toString())

    writeAndClose(ch, httpResponse)
  }
}
