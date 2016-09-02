package osago.bb.api

import java.net.URL
import java.nio.charset.Charset
import java.util.Calendar

import com.typesafe.config.ConfigFactory
import org.slf4j.LoggerFactory
import ru.dogma.http.HttpBrowser

import scala.collection.mutable
import scala.xml.{Elem, XML}

import scala.collection.JavaConversions._

import ru.dogma.xml._

/**
  * Created by rudogma on 05.05.16.
  */
object TerminalsApi {

  lazy val config = ConfigFactory.load()

  val logger = LoggerFactory.getLogger("api.terminals")

  val UTF8 = Charset.forName("UTF-8")
  val GATEWAY = new URL(config.getString("terminals.gateway"))

  def photo_uploaded(id_terminal:Int, uploaded_at:Calendar): Unit ={
    val params = mutable.Map(
      "cmd" -> "terminals.photo_uploaded",
      "id_terminal" -> id_terminal,
      "uploaded_at" -> (uploaded_at.getTimeInMillis / 1000)
    )

    post(params)
  }

  def screenshot_uploaded(id_terminal:Int, uploaded_at:Calendar): Unit ={
    val params = mutable.Map(
      "cmd" -> "terminals.screenshot_uploaded",
      "id_terminal" -> id_terminal,
      "uploaded_at" -> (uploaded_at.getTimeInMillis / 1000)
    )

    post(params)
  }


  def post(params: mutable.Map[String, Any]): Elem ={

    logger.debug("[SEND] %s" format (params.map( p => (p._1, "%s=%s" format (p._1, p._2))).mkString("\n")))

    val browser = new HttpBrowser(false)
    browser.navigate(GATEWAY, params.map( p => (p._1 -> p._2.toString) ))

    val response = browser.getContent

    logger.debug("[GOT] %s" format response)

    val responseXML = XML.loadString(response)

    require( (responseXML attr "status").toInt == 200, "Response with error")

    responseXML
  }
}
