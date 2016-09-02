package logging

import org.slf4j.LoggerFactory
import ru.dogma.netty.http.{FullHttpRequest, FullHttpResponse}

/**
 * Created.
 */
class HttpServerLogger(accessCategory:String, errorsCategory:String) {

  def this(accessCategory:String){
    this(accessCategory, accessCategory)
  }

  val accessLogger = LoggerFactory.getLogger(accessCategory)
  val errorsLogger = LoggerFactory.getLogger(errorsCategory)


  def access(httpRequest:FullHttpRequest, httpResponse:FullHttpResponse): Unit ={

    accessLogger.debug("%s OK    %5dms %s %s" format (
      httpRequest.getMethod,
      (System.currentTimeMillis() - httpRequest.createdAt),
      httpRequest.getUri,
      httpRequest.postParams.toString()
      ))
  }

  def error(httpRequest:FullHttpRequest, httpResponse:FullHttpResponse, errorMessage:String, error:Throwable): Unit ={

    errorsLogger.debug("%s ERROR %5dms %s %s %s" format (
      httpRequest.getMethod,
      (System.currentTimeMillis() - httpRequest.createdAt),
      httpRequest.getUri,
      httpRequest.postParams.toString(),
      errorMessage
      ),
    error)
  }
}
