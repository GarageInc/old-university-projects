
LIBS_DIR="/work/work/maven-example/lib"
CP=""

# Echo out all files in directory!
for file in ${LIBS_DIR}/*.jar ; do
 if [ -n "${CP}" ]; then
  CP=${CP}":${file}"
 else
  CP=${CP}"${file}"
 fi
done

CP=${CP}


java -Dorg.apache.commons.logging.Log=org.apache.commons.logging.impl.SimpleLog \
	-Dorg.apache.commons.logging.simplelog.showdatetime=true \
	-Dorg.apache.commons.logging.simplelog.log.httpclient.wire=debug \
	-Dorg.apache.commons.logging.simplelog.log.org.apache.commons.httpclient=debug \
	-Dfile.encoding=UTF-8  \
	-classpath ${CP}:osago-daemon-1.0.jar \
	ThumbnailsFactory 400 225 "/work/work/image.jpg" "/work/work/th_image.jpg"

