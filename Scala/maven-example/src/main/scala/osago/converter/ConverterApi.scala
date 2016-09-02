package osago.converter

import isc.api.http.{HttpApi, HttpService}

/**
  * Created by rudogma on 15.05.16.
  */
object ConverterApi extends HttpApi {

  override type ServiceBase = HttpService

  val services:ServiceMap = {
    case "converter" => classOf[ ConverterService ]
  }
}
