#pragma once

#include <Math.h>
#include <cstdlib>
#include<cstring>


using namespace std;
namespace AnastasijaPaderina {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;
	
	#include"Complex.cpp"
	/// <summary>
	/// Сводка для Form1
	/// </summary>
	
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^  label1;
	protected: 
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::Button^  button2;
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::TextBox^  textBox2;
	private: System::Windows::Forms::TextBox^  textBox3;
	private: System::Windows::Forms::TextBox^  textBox4;
	private: System::Windows::Forms::Label^  label5;
	public: System::Windows::Forms::Label^  label6;
	private: 
	private: System::Windows::Forms::TextBox^  textBox5;
	private: System::Windows::Forms::Label^  label7;
	private: System::Windows::Forms::RichTextBox^  richTextBox1;
	public: 

	protected: 

	private:
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->textBox3 = (gcnew System::Windows::Forms::TextBox());
			this->textBox4 = (gcnew System::Windows::Forms::TextBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->textBox5 = (gcnew System::Windows::Forms::TextBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(59, 44);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(107, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Заданная точность:";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(59, 75);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(234, 13);
			this->label2->TabIndex = 1;
			this->label2->Text = L"Действительная часть комплексного числа:";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(59, 107);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(190, 13);
			this->label3->TabIndex = 2;
			this->label3->Text = L"Мнимая часть комплексного числа:";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(59, 194);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(430, 13);
			this->label4->TabIndex = 3;
			this->label4->Text = L"Результат вычислений с разложением в ряд Тейлора и учётом заданной точности:";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(62, 164);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(301, 23);
			this->button1->TabIndex = 4;
			this->button1->Text = L"ВЫЧИСЛИТЬ ТАНГЕНС КОМПЛЕКСНОГО ЧИСЛА";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(508, 33);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(301, 23);
			this->button2->TabIndex = 5;
			this->button2->Text = L"Открыть файл для загрузки комплексного числа";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &Form1::button2_Click);
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(312, 36);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(100, 20);
			this->textBox1->TabIndex = 6;
			this->textBox1->Text = L"1";
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(312, 68);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(100, 20);
			this->textBox2->TabIndex = 7;
			this->textBox2->Text = L"0";
			this->textBox2->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// textBox3
			// 
			this->textBox3->Location = System::Drawing::Point(312, 104);
			this->textBox3->Name = L"textBox3";
			this->textBox3->Size = System::Drawing::Size(100, 20);
			this->textBox3->TabIndex = 8;
			this->textBox3->Text = L"0";
			this->textBox3->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// textBox4
			// 
			this->textBox4->Location = System::Drawing::Point(538, 187);
			this->textBox4->Name = L"textBox4";
			this->textBox4->Size = System::Drawing::Size(115, 20);
			this->textBox4->TabIndex = 9;
			this->textBox4->Text = L"0";
			this->textBox4->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(659, 190);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(13, 13);
			this->label5->TabIndex = 10;
			this->label5->Text = L"+";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(793, 194);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(16, 13);
			this->label6->TabIndex = 11;
			this->label6->Text = L"* i";
			// 
			// textBox5
			// 
			this->textBox5->Location = System::Drawing::Point(678, 187);
			this->textBox5->Name = L"textBox5";
			this->textBox5->Size = System::Drawing::Size(109, 20);
			this->textBox5->TabIndex = 12;
			this->textBox5->Text = L"0";
			this->textBox5->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(61, 20);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(428, 13);
			this->label7->TabIndex = 13;
			this->label7->Text = L"КОМПЛЕКСНОЕ ЧИСЛО Z ДОЛЖНО БЫТЬ МЕНЬШЕ Pi/2(условие по Википедии)!";
			// 
			// richTextBox1
			// 
			this->richTextBox1->Location = System::Drawing::Point(508, 62);
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->Size = System::Drawing::Size(301, 119);
			this->richTextBox1->TabIndex = 14;
			this->richTextBox1->Text = L"";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(896, 279);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->textBox5);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->textBox4);
			this->Controls->Add(this->textBox3);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Name = L"Form1";
			this->Text = L"Разложение по Тейлору";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

			//Рассчитываем по Тейлору
		//tg(z)=SUM((-1)*2*n/z) (если рассматривать тангенс как отношение синуса Z к косинусу Z)
		//В результате-тангенс тоже комплексное число
	public: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
			 double real, imag,eps;//eps-заданная точность

			 //Переводим из окошков данные в наши объекты(это вещественные и действительные части числа)
			 real=Convert::ToDouble(textBox2->Text);
			 imag=Convert::ToDouble(textBox3->Text);
			 Complex z(real,imag);
			 
			 eps=Convert::ToDouble(textBox1->Text);//Переводим из строки в дробное число

			 Complex an=z;
			 Complex tg(0,0);//Наш результат, который отобразится на форме
			 int n=1;//Счетчик обычный
			 //Сделано по википедии. 
			 //Разложение тангенсТО ЕСТЬ РАССМАТРИВАЕМЫЙ ОБЪЕКТ X должен быть меньше по модулю Pi/2
			 //https://ru.wikipedia.org/wiki/%D0%A0%D1%8F%D0%B4_%D0%A2%D0%B5%D0%B9%D0%BB%D0%BE%D1%80%D0%B0#.D0.A1.D0.B2.D1.8F.D0.B7.D0.B0.D0.BD.D0.BD.D1.8B.D0.B5_.D0.BE.D0.BF.D1.80.D0.B5.D0.B4.D0.B5.D0.BB.D0.B5.D0.BD.D0.B8.D1.8F
			 while(Zabs(an)>eps)//Цикл, который работает, пока значение не станет меньше заданной точности
			 {
				 an=((getBern(2*n))*POW(-4, n) * (1 - POW(4, n)) * (POWZ(an,(2*n)-1)))/fact(2*n);  
				 
				 tg=tg+an;
				 n++;
			}

			 //Переводим наше число(РЕЗУЛЬТАТ tg(z)) в строку и отправляем в текстовое окошко
			 textBox4->Text=tg.getreal()+"";
			 textBox5->Text=tg.getimag()+"";
			 }
		
			//Возведение в степень комплексного числа
			public: Complex POWZ(Complex z, int n){
					Complex c=z;
					for(int i=1; i<n; i++)
						c=c*z;
					return c;
					}
			//Возведение в степень обычного десятичного числа
			public: int POW(int z, int n){
					int c=z;
					for(int i=1; i<n; i++)
						c=c*z;
					return c;
					}
			//Модуль комплексного числа
			public: double Zabs(Complex z){
				double d;
				d=sqrt(z.getreal()*z.getreal()+z.getimag()*z.getimag());
				return d;
			}

			//Числа Бернули
			public: double getBern(int n)
			{
				double bern;

				if (n == 0)
				{
					bern = 1.0;
				}
				else
				{
					bern = 0.0;
					for (int k = 1; k <= n;k++ )
					{
						bern +=(-1)/Convert::ToDouble(n+1)*fact(n+1)/(fact(k+1)*fact(n-k))*getBern(n-k);
					}
				}
				return bern;
			}

            //Факториал
			public: double fact(int k)
			{
				if (k == 0)
				{
					return 1;
				}
				else
				{
					return k * fact(k - 1);
				}
			}

			 //Если пользователь нажал - "выбрать из файла" - то загружаем данные из файла в окошки на форме(туда, где отображаются действительная и вещественная части числа).
			private: System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) {
						  Stream^ myStream;
                          OpenFileDialog^ openFileDialog1 = gcnew OpenFileDialog;
                          
                          openFileDialog1->Filter = "Secret key(*.txt)|*.txt";
                          openFileDialog1->FilterIndex = 2;
                          openFileDialog1->RestoreDirectory = true;
						  
                          if ( openFileDialog1->ShowDialog() == System::Windows::Forms::DialogResult::OK )
                          {
                                 if ( (myStream = openFileDialog1->OpenFile()) != nullptr )
                                 {
                                        StreamReader^ sw = gcnew StreamReader(myStream);
                                        String ^str1="";
                                        str1=sw->ReadToEnd();
                                        richTextBox1->Text=(str1);
                                             
										textBox2->Text="";//Обнуляем. Туда будем считывать свои данные
										textBox3->Text="";
										//Получив первые две строки - мы её "раскидываем" по окошкам, тем самый даем пользователю выбор для автоматического распределения своих данных
										//Максимальная длина числа - считаем, что 1000.

										//ТУТ ЖЕ ВВОДИМ КОНСТРУКЦИЮ Try/Catch - если в файле будут ошибочные данные(чтение будет ошибочным), то выйдет соответствующее сообщение(УВЕДОМЛЕНИЕ ОБ ОШИБКЕ)
										//Необходимо по условию задачи и помогает "видеть" неправильные данные
										try
										{
											int i=0;
											for(; i<1000 && str1[i]!='\r'; i++)
											{
												textBox2->Text+=str1[i];
											}
											i=i+2;
											//Считываем второе число из общей строки
										
											for(int j=0;  str1[i]!='\r' ; j++,i++)//Условие может привести к ошибке.
											{
												textBox3->Text+=str1[i];
											}

											textBox2->Text+="";
												textBox3->Text+="";
										}
										catch(System::Exception^ ex)
										{//Иначе - сообщение об ошибке и возврат к начальному состоянию
											textBox2->Text="0";
												textBox2->Text="0";
											textBox3->Text+="";
												textBox3->Text+="";
											MessageBox::Show(ex->ToString(), "Ошибка! Проверьте правильность данных в файле!", MessageBoxButtons::OK, MessageBoxIcon::Error);
										}
								 }
                          }
				 }
		};
}

