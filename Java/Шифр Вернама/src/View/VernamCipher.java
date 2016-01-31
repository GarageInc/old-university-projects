package View;

import Controller.Contr;
import Controller.IContr;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.io.UnsupportedEncodingException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.application.Platform;
import javafx.embed.swing.JFXPanel;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.StackPane;
import javafx.scene.text.Font;
import javafx.scene.text.FontWeight;
import javafx.scene.text.Text;
import javax.swing.JApplet;
import javax.swing.JFrame;
import javax.swing.SwingUtilities;
import javax.swing.UIManager;

/**
 *
 * @author 7
 */
public class VernamCipher extends JApplet {
    
    private static final int JFXPANEL_WIDTH_INT = 500;
    private static final int JFXPANEL_HEIGHT_INT = 400;
    private static JFXPanel fxContainer;

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            
            @Override
            public void run() {
                try {
                    UIManager.setLookAndFeel("com.sun.java.swing.plaf.nimbus.NimbusLookAndFeel");
                } catch (Exception e) {
                }
                
                JFrame frame = new JFrame("Мой шифр");
                frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
                
                JApplet applet = new VernamCipher();
                applet.init();
                
                frame.setContentPane(applet.getContentPane());
                
                frame.pack();
                frame.setLocationRelativeTo(null);
                frame.setVisible(true);
                
                applet.start();
            }
        });
    }
    
    @Override
    public void init() {
        fxContainer = new JFXPanel();
        fxContainer.setPreferredSize(new Dimension(JFXPANEL_WIDTH_INT, JFXPANEL_HEIGHT_INT));
        add(fxContainer, BorderLayout.CENTER);
        // create JavaFX scene
        Platform.runLater(new Runnable() {
            
            @Override
            public void run() {
                createScene();
            }
        });
    }
    
    private void createScene() {
        GridPane grid = new GridPane();
        grid.setAlignment(Pos.CENTER);
        grid.setHgap(10);
        grid.setVgap(10);
        grid.setPadding(new Insets(50, 50, 50, 50));

        Text scenetitle = new Text("Мой матричный шифр");
        scenetitle.setFont(Font.font("Tahoma", FontWeight.NORMAL, 20));
        grid.add(scenetitle, 0, 0, 2, 1);

        Label userName = new Label("Введите имя пользователя:");
        grid.add(userName, 0, 1);

        TextField userTextField = new TextField();
        grid.add(userTextField, 1, 1);

        Label pw = new Label("Текст:");
        grid.add(pw, 0, 2);

        TextField pwBox = new TextField();
        grid.add(pwBox, 1, 2);
        
        Label message = new Label("Результат:");
        grid.add(message, 0, 3);

        Label messageField = new Label();
        grid.add(messageField, 1, 3);
        
        
        Label enctypted = new Label("Зашифрованный текст:");
        grid.add(enctypted, 0, 5);

        TextField encryptedField = new TextField();
        grid.add(encryptedField, 1, 5);
        
        Label dectypted = new Label("Расшифрованный текст:");
        grid.add(dectypted, 0, 6);

        TextField decryptedField = new TextField();
        grid.add(decryptedField, 1, 6);
        
        Button btn = new Button("Зашифровать");
        btn.setOnAction(new EventHandler<ActionEvent>() {
            
            @Override
            public void handle(ActionEvent event) {
                
                        // Создаем шифратор
                        Contr contr = IContr.CreateContr();
                        
                        // Получим ключ пользователя
                        String key = contr.GetKey(userTextField.getText());

                            if (!"error".equals(key))
                            {
                                try {
                                    // Инициализация пользователя
                                    // Считываем текст
                                    contr.InitUser(pwBox.getText(),key);
                                } catch (UnsupportedEncodingException ex) {
                                    messageField.setText("Не знаю, друг, какая тут ошибка ввода");
                                }
                                
                                String coded = contr.codedString;
                                String decoded = contr.decodedString;
                                
                                encryptedField.setText(coded);
                                decryptedField.setText(decoded);                                                             
                            }
                            else
                            {
                                messageField.setText("Файл пользователя не найден");
                            }
            }
        });
        
        HBox hbBtn = new HBox(10);
        hbBtn.setAlignment(Pos.BOTTOM_RIGHT);
        hbBtn.getChildren().add(btn);
        grid.add(hbBtn, 1, 4);
        
        
        StackPane root = new StackPane();
        root.getChildren().add(grid);
        fxContainer.setScene(new Scene(root));
    }
    
}
