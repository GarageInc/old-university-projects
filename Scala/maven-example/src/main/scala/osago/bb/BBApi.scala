package osago.bb

import isc.api.http.{HttpApi, HttpService}

/**
  * Created by rudogma on 05.05.16.
  */
object BBApi extends HttpApi{

  override type ServiceBase = HttpService

  val services:ServiceMap = {
    case "bb" => classOf[ BBService ]
  }
}
