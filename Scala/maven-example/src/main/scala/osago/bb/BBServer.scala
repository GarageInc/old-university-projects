package osago.bb

import java.nio.channels.ClosedChannelException

import akka.actor.ActorSystem
import io.netty.channel.Channel
import io.netty.handler.codec.http.HttpMethod
import isc.FaviconException
import isc.api.http.HttpToXmlApiServer
import logging.HttpServerLogger
import ru.dogma.netty.http.{FullHttpRequest, FullHttpResponse}

/**
  * Created by rudogma on 05.05.16.
  */
object BBServer extends HttpToXmlApiServer{

  val api = BBApi

  val system = ActorSystem("bb-server")

  override val RESPONSE_DEFAULT_HEADERS = Map(
    "Access-Control-Allow-Origin" -> "*",
    "Access-Control-Allow-Headers" -> "X-Requested-With, Content-Type"
  )

  protected val httpLogger = new HttpServerLogger("access.bb","errors.bb")

  override def handler_request( ch:Channel, httpRequest:FullHttpRequest ):Unit = {

    if(httpRequest.getMethod == HttpMethod.OPTIONS){
//      println("Response OPTIONS method")
//      val httpResponse = new FullHttpResponse()
//
//      if(RESPONSE_DEFAULT_HEADERS != null){
//        for( (key,value) <- RESPONSE_DEFAULT_HEADERS){
//          httpResponse.headers().set(key, value)
//        }
//      }

      writeAndClose(ch, createHttpResponse())
    }else{
      super.handler_request(ch, httpRequest)
    }
  }

  override def handler_exception(ch: Channel, httpRequest:FullHttpRequest): PartialFunction[Throwable, Unit] = {

    case e:FaviconException => {
      writeError(ch, 500, e.getMessage)
    }

    case e:Throwable => {
      httpLogger.error(httpRequest, null, e.getMessage, e)

      if( !e.isInstanceOf[ ClosedChannelException ]){
        writeError(ch, 500, e.getMessage)
      }
    }
  }

  def writeError(ch:Channel, errorCode:Int, errorMessage:String): Unit ={

    val response = <x status={errorCode.toString} msg={errorMessage} />

    val httpResponse = createHttpResponse().replaceContent(response.toString())

    writeAndClose(ch, httpResponse)
  }

  override def requestCompleted(ch: Channel, httpRequest: FullHttpRequest, httpResponse: BBApi.Response): Unit = {
    httpLogger.access(httpRequest, httpResponse)
  }
}
