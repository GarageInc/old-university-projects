package logging;

import ch.qos.logback.classic.spi.ILoggingEvent;
import ch.qos.logback.core.sift.AbstractDiscriminator;

/**
 * Created.
 */
public class MyDiscriminator extends AbstractDiscriminator<ILoggingEvent> {
    private static final String KEY = "contextName";
    private String defaultValue;

    public MyDiscriminator() {
    }

    public String getDiscriminatingValue(ILoggingEvent event) {
        return event.getLoggerName();

//        ContextSelector selector = ContextSelectorStaticBinder.getSingleton().getContextSelector();
//        if (selector == null) {
//            return "selector-null";
////            return this.defaultValue;
//        } else {
//            LoggerContext lc = selector.getLoggerContext();
//
//            System.out.println("LC: "+ (lc == null));
//            System.out.println("N: "+lc.getName());
//
//            return "selector-not-null";
////            return lc == null ? this.defaultValue : lc.getName();
//        }
    }

    public String getKey() {
        return "contextName";
    }

    public void setKey(String key) {
        throw new UnsupportedOperationException("Key cannot be set. Using fixed key contextName");
    }

    public String getDefaultValue() {
        return this.defaultValue;
    }

    public void setDefaultValue(String defaultValue) {
        this.defaultValue = defaultValue;
    }
}
