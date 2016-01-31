#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "QTextStream"
#include "QTextCodec"
#include "QFile"
#include "QTime"

QFile file("test.dat");
QTextStream stream(&file);
QStringList questions;
QStringList answers;
QList<int> qarr;    //массив индексов неверных ответов
int question;       //номер вопроса
int re;             //повтор вопроса


//процдура выбора и вывода вопроса
// f - false для генерирования номера вопроса
void AskQuestion(QLineEdit *qle,bool f)
{
    //проверить кол-во вопросов
   if (!questions.length())
   {
       qle->setText("Вопросы отсутствуют!");return;
   }

   //сгенерировать номер вопроса если false
   if ( !f ) question = qrand() % questions.length();
   //Иначе выбрать вопрос для закрепления из списка неотвеченных
   else
   {
       question = qarr.first(); //первый по списку номер вопроса
       qarr.removeFirst(); //удалить его из списка
   }
   qle->setText(questions[question]); //вывести сам вопрос

}


MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    //задать кодировку для отображения кириллицы
    QTextCodec::setCodecForLocale(QTextCodec::codecForName("UTF-8"));

    //взять в переменную заданное число для повтора вопроса
    re = ui->lineEdit_4->text().toInt();

    //открыть файл вопросов для чтения
    file.open(QIODevice::ReadOnly);

    //считывать строки пока не конец файла
    while (!stream.atEnd())
    {
        //считать строку
        QString str = stream.readLine();
        //разбить строку на вопрос и ответ
        QStringList lst = str.split(" ?; ");
        //добавить в список вопросов и ответов
        questions.append(lst[0]);
        answers.append(lst[1]);
    }

    //инициализация генератора случайных чисел текущим временем
    qsrand(QTime(0,0,0).secsTo(QTime::currentTime()));
    AskQuestion(ui->lineEdit, false);
}

MainWindow::~MainWindow()
{
    delete ui;
}

//ответ на вопрос
void MainWindow::on_pushButton_clicked()
{

    //разбить ответ на отдельные лексемы и проверять каждое
    QStringList srcansw = answers[question].split(QRegExp("( )+")); //правильный ответ
    QStringList dstansw = ui->lineEdit_2->text().split(QRegExp("( )+")); //ответ пользователя

    bool f = true;
    //провести сравнение ответов
    if (dstansw.length() == 3) //если лексем 3
    {
        if ( srcansw[0].toInt() != dstansw[0].toInt()) f = false; //сравнить число
        if ( srcansw[1] != dstansw[1] ) f = false; //название месяца
        if ( srcansw[2].toInt() != dstansw[2].toInt())  f = false; //и года
    }
    else f = false; //если меньше или больше 3 то false


    if ( f )    ui->lineEdit_3->setText( "Ответ верный! " + answers[question] );
    else
    {
        ui->lineEdit_3->setText( "Ответ неверный! " + answers[question] );
        qarr.append( question ); //добавить в массив неотвеченных вопросов индекс вопроса
    }

    //если массив с неотвеченными не пустой
    if (qarr.length() > 0)
    {
        if (!re) //проверить счетчик перед повтором старого вопроса
        {
            //если он в 0,опять записать значение
            re = ui->lineEdit_4->text().toInt();
            //задать вопрос из списка неотвечяенных
            AskQuestion(ui->lineEdit,true);
            return;
        }
        //декремент счетчика перед повтором вопроса
        re--;
    }
    //иначе выбрать вопрос из списка вопросов
    AskQuestion(ui->lineEdit,false);
}
