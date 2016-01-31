package View;

import Controller.IContr;
import Controller.Contr;
import javafx.application.Platform;
import javafx.embed.swing.JFXPanel;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.Text;
import javax.swing.JFrame;
import java.io.UnsupportedEncodingException;
       
public class Program {
    
        private static void initAndShowGUI() {
        // This method is invoked on the EDT thread
        JFrame frame = new JFrame("Swing and JavaFX");
        final JFXPanel fxPanel = new JFXPanel();
        frame.add(fxPanel);
        frame.setSize(300, 200);
        frame.setVisible(true);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        Platform.runLater(new Runnable() {
            @Override
            public void run() {
                initFX(fxPanel);
            }
       });
    }

    private static void initFX(JFXPanel fxPanel) {
        // This method is invoked on the JavaFX thread
        Scene scene = createScene();
        fxPanel.setScene(scene);
    }

    private static Scene createScene() {
        Group  root  =  new  Group();
        Scene  scene  =  new  Scene(root, Color.ALICEBLUE);
        Text  text  =  new  Text();
        
        text.setX(40);
        text.setY(100);
        text.setFont(new Font(25));
        text.setText("Welcome JavaFX!");

        root.getChildren().add(text);

        return (scene);
    }

		public static void main (String [] args) throws UnsupportedEncodingException
		{
                    
                        SwingUtilities.invokeLater(new Runnable() {
                            @Override
                            public void run() {
                                initAndShowGUI();
                            }
                        });

                        // Создаем шифратор
                        Contr contr = IContr.CreateContr();
                        
                        boolean authorization=false;
                        while(!authorization)
                        {
                            // Считывание ключа
                            System.out.println("\nВведите имя пользователя");
                            // Получим ключ пользователя
                            String key = contr.TryGetKey();

                            if (!"error".equals(key))
                            {
                                // Инициализация пользователя
                                // Считываем текст
                                System.out.println("\nВведите шифруемый текст");
                                contr.InitUser(key);
                                
                                String coded = contr.codedString;
                                String decoded = contr.decodedString;
                                
                                System.out.println("\nЗашифрованный текст: "+coded);//Charset.defaultCharset()
                                System.out.println("\nРасшифрованный текст: "+decoded);
                                
                                authorization=true;                                
                            }
                        }
                        
		}
	}
