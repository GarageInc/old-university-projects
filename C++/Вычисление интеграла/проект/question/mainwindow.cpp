#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "QFile"
#include "QTextStream"

QFile file("test.dat");
QTextStream stream(&file);

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    //открыть файл для чтения записи
    file.open( QIODevice::ReadWrite);
    file.seek(0); //установить указатель в начало файла
    while (!stream.atEnd()) //читать построчно пока не считан все строки файла
    {
        //считать строку из потока файла
        QString str = stream.readLine();
        //составить список вопрос-ответ
        QStringList list = str.split(" ?; ");
        //вывести в qtextedit
        ui->textEdit->append( list[0] + list[1] );
    }

}

MainWindow::~MainWindow()
{
    delete ui;
}


//ввод/вывод вопросов
void MainWindow::on_pushButton_clicked()
{
    //
    QString str = ui->lineEdit->text() + " ?; " + ui->lineEdit_2->text() + "\n";
    //записать вопрос-ответ в файл ( разделитель ?: )
    stream << str;
    //вывести его в qtext
    ui->textEdit->append(ui->lineEdit->text() + ui->lineEdit_2->text() );
}

//закрыть перед выходом
void MainWindow::on_MainWindow_destroyed()
{
    file.close();
}

//очистить файл
void MainWindow::on_pushButton_2_clicked()
{
    file.resize(0);
    ui->textEdit->clear();
}
