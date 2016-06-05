#pragma once
#include<fstream>
#include<windows.h>
#include <math.h>
namespace ryadu {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

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
	private: System::Windows::Forms::GroupBox^  groupBox1;
	private: System::Windows::Forms::RadioButton^  rb3;
	protected: 

	private: System::Windows::Forms::RadioButton^  rb2;

	private: System::Windows::Forms::RadioButton^  rb1;

	private: System::Windows::Forms::DataGridView^  table;




	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::Button^  button2;




	private: System::Windows::Forms::Label^  label1;

	private: System::Windows::Forms::PictureBox^  pictureBox2;
	private: System::Windows::Forms::PictureBox^  pictureBox1;

	private: System::Windows::Forms::DataGridViewTextBoxColumn^  L1;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  L2;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  L3;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  LX;
	private: System::Windows::Forms::RadioButton^  rb4;
	private: System::Windows::Forms::PictureBox^  pictureBox6;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::PictureBox^  pictureBox5;

	private: System::Windows::Forms::RadioButton^  rb5;
	private: System::Windows::Forms::PictureBox^  pictureBox8;
	private: System::Windows::Forms::PictureBox^  pictureBox7;
	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::RadioButton^  rb6;
	private: System::Windows::Forms::PictureBox^  pictureBox9;
	private: System::Windows::Forms::PictureBox^  pictureBox10;
	private: System::Windows::Forms::Label^  label5;
	private: System::Windows::Forms::Label^  helpl;

	private: System::Windows::Forms::PictureBox^  pictureBox4;
	private: System::Windows::Forms::PictureBox^  pictureBox3;






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
			System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid));
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->pictureBox3 = (gcnew System::Windows::Forms::PictureBox());
			this->pictureBox9 = (gcnew System::Windows::Forms::PictureBox());
			this->rb6 = (gcnew System::Windows::Forms::RadioButton());
			this->pictureBox8 = (gcnew System::Windows::Forms::PictureBox());
			this->rb5 = (gcnew System::Windows::Forms::RadioButton());
			this->pictureBox6 = (gcnew System::Windows::Forms::PictureBox());
			this->rb4 = (gcnew System::Windows::Forms::RadioButton());
			this->pictureBox2 = (gcnew System::Windows::Forms::PictureBox());
			this->pictureBox1 = (gcnew System::Windows::Forms::PictureBox());
			this->rb3 = (gcnew System::Windows::Forms::RadioButton());
			this->rb2 = (gcnew System::Windows::Forms::RadioButton());
			this->rb1 = (gcnew System::Windows::Forms::RadioButton());
			this->table = (gcnew System::Windows::Forms::DataGridView());
			this->L1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->L2 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->L3 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->LX = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->pictureBox5 = (gcnew System::Windows::Forms::PictureBox());
			this->pictureBox7 = (gcnew System::Windows::Forms::PictureBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->pictureBox10 = (gcnew System::Windows::Forms::PictureBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->helpl = (gcnew System::Windows::Forms::Label());
			this->pictureBox4 = (gcnew System::Windows::Forms::PictureBox());
			this->groupBox1->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox3))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox9))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox8))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox6))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox2))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->table))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox5))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox7))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox10))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox4))->BeginInit();
			this->SuspendLayout();
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->pictureBox3);
			this->groupBox1->Controls->Add(this->pictureBox9);
			this->groupBox1->Controls->Add(this->rb6);
			this->groupBox1->Controls->Add(this->pictureBox8);
			this->groupBox1->Controls->Add(this->rb5);
			this->groupBox1->Controls->Add(this->pictureBox6);
			this->groupBox1->Controls->Add(this->rb4);
			this->groupBox1->Controls->Add(this->pictureBox2);
			this->groupBox1->Controls->Add(this->pictureBox1);
			this->groupBox1->Controls->Add(this->rb3);
			this->groupBox1->Controls->Add(this->rb2);
			this->groupBox1->Controls->Add(this->rb1);
			this->groupBox1->Location = System::Drawing::Point(22, 27);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(764, 164);
			this->groupBox1->TabIndex = 0;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"Выбор";
			// 
			// pictureBox3
			// 
			this->pictureBox3->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox3.Image")));
			this->pictureBox3->Location = System::Drawing::Point(454, 12);
			this->pictureBox3->Name = L"pictureBox3";
			this->pictureBox3->Size = System::Drawing::Size(171, 58);
			this->pictureBox3->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox3->TabIndex = 14;
			this->pictureBox3->TabStop = false;
			// 
			// pictureBox9
			// 
			this->pictureBox9->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox9.Image")));
			this->pictureBox9->Location = System::Drawing::Point(454, 76);
			this->pictureBox9->Name = L"pictureBox9";
			this->pictureBox9->Size = System::Drawing::Size(171, 65);
			this->pictureBox9->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox9->TabIndex = 13;
			this->pictureBox9->TabStop = false;
			this->pictureBox9->Click += gcnew System::EventHandler(this, &Form1::pictureBox9_Click);
			// 
			// rb6
			// 
			this->rb6->AutoSize = true;
			this->rb6->Location = System::Drawing::Point(414, 76);
			this->rb6->Name = L"rb6";
			this->rb6->Size = System::Drawing::Size(34, 17);
			this->rb6->TabIndex = 12;
			this->rb6->TabStop = true;
			this->rb6->Text = L"е)";
			this->rb6->UseVisualStyleBackColor = true;
			// 
			// pictureBox8
			// 
			this->pictureBox8->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox8.Image")));
			this->pictureBox8->Location = System::Drawing::Point(259, 76);
			this->pictureBox8->Name = L"pictureBox8";
			this->pictureBox8->Size = System::Drawing::Size(117, 45);
			this->pictureBox8->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox8->TabIndex = 11;
			this->pictureBox8->TabStop = false;
			// 
			// rb5
			// 
			this->rb5->AutoSize = true;
			this->rb5->Location = System::Drawing::Point(210, 76);
			this->rb5->Name = L"rb5";
			this->rb5->Size = System::Drawing::Size(34, 17);
			this->rb5->TabIndex = 8;
			this->rb5->TabStop = true;
			this->rb5->Text = L"д)";
			this->rb5->UseVisualStyleBackColor = true;
			// 
			// pictureBox6
			// 
			this->pictureBox6->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox6.Image")));
			this->pictureBox6->Location = System::Drawing::Point(47, 76);
			this->pictureBox6->Name = L"pictureBox6";
			this->pictureBox6->Size = System::Drawing::Size(87, 45);
			this->pictureBox6->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox6->TabIndex = 7;
			this->pictureBox6->TabStop = false;
			this->pictureBox6->Click += gcnew System::EventHandler(this, &Form1::pictureBox6_Click);
			// 
			// rb4
			// 
			this->rb4->AutoSize = true;
			this->rb4->Location = System::Drawing::Point(10, 76);
			this->rb4->Name = L"rb4";
			this->rb4->Size = System::Drawing::Size(33, 17);
			this->rb4->TabIndex = 6;
			this->rb4->TabStop = true;
			this->rb4->Text = L"г)";
			this->rb4->UseVisualStyleBackColor = true;
			// 
			// pictureBox2
			// 
			this->pictureBox2->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox2.Image")));
			this->pictureBox2->Location = System::Drawing::Point(259, 12);
			this->pictureBox2->Name = L"pictureBox2";
			this->pictureBox2->Size = System::Drawing::Size(129, 46);
			this->pictureBox2->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox2->TabIndex = 4;
			this->pictureBox2->TabStop = false;
			this->pictureBox2->Click += gcnew System::EventHandler(this, &Form1::pictureBox2_Click);
			// 
			// pictureBox1
			// 
			this->pictureBox1->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox1.Image")));
			this->pictureBox1->Location = System::Drawing::Point(50, 12);
			this->pictureBox1->Name = L"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(84, 30);
			this->pictureBox1->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox1->TabIndex = 3;
			this->pictureBox1->TabStop = false;
			// 
			// rb3
			// 
			this->rb3->AutoSize = true;
			this->rb3->Location = System::Drawing::Point(414, 19);
			this->rb3->Name = L"rb3";
			this->rb3->Size = System::Drawing::Size(34, 17);
			this->rb3->TabIndex = 2;
			this->rb3->TabStop = true;
			this->rb3->Text = L"в)";
			this->rb3->UseVisualStyleBackColor = true;
			// 
			// rb2
			// 
			this->rb2->AutoSize = true;
			this->rb2->Location = System::Drawing::Point(210, 17);
			this->rb2->Name = L"rb2";
			this->rb2->Size = System::Drawing::Size(34, 17);
			this->rb2->TabIndex = 1;
			this->rb2->TabStop = true;
			this->rb2->Text = L"б)";
			this->rb2->UseVisualStyleBackColor = true;
			// 
			// rb1
			// 
			this->rb1->AutoSize = true;
			this->rb1->Location = System::Drawing::Point(10, 17);
			this->rb1->Name = L"rb1";
			this->rb1->Size = System::Drawing::Size(34, 17);
			this->rb1->TabIndex = 0;
			this->rb1->TabStop = true;
			this->rb1->Text = L"а)";
			this->rb1->UseVisualStyleBackColor = true;
			// 
			// table
			// 
			this->table->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(4) {this->L1, this->L2, this->L3, 
				this->LX});
			this->table->Location = System::Drawing::Point(32, 228);
			this->table->Name = L"table";
			this->table->Size = System::Drawing::Size(615, 330);
			this->table->TabIndex = 1;
			// 
			// L1
			// 
			this->L1->HeaderText = L"L1";
			this->L1->Name = L"L1";
			this->L1->ReadOnly = true;
			this->L1->Width = 140;
			// 
			// L2
			// 
			this->L2->HeaderText = L"L2";
			this->L2->Name = L"L2";
			this->L2->ReadOnly = true;
			this->L2->Width = 140;
			// 
			// L3
			// 
			this->L3->HeaderText = L"L1/L2";
			this->L3->Name = L"L3";
			this->L3->ReadOnly = true;
			this->L3->Width = 140;
			// 
			// LX
			// 
			this->LX->HeaderText = L"X";
			this->LX->Name = L"LX";
			this->LX->ReadOnly = true;
			this->LX->Width = 140;
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(682, 91);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(104, 29);
			this->button1->TabIndex = 2;
			this->button1->Text = L"Расчёт";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(682, 139);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(104, 29);
			this->button2->TabIndex = 3;
			this->button2->Text = L"Очистка";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &Form1::button2_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(679, 194);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(41, 13);
			this->label1->TabIndex = 4;
			this->label1->Text = L"Статус";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(653, 240);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(37, 13);
			this->label2->TabIndex = 6;
			this->label2->Text = L"а, б, в";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(668, 310);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(12, 13);
			this->label3->TabIndex = 7;
			this->label3->Text = L"г";
			// 
			// pictureBox5
			// 
			this->pictureBox5->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox5.Image")));
			this->pictureBox5->Location = System::Drawing::Point(697, 310);
			this->pictureBox5->Name = L"pictureBox5";
			this->pictureBox5->Size = System::Drawing::Size(120, 61);
			this->pictureBox5->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox5->TabIndex = 8;
			this->pictureBox5->TabStop = false;
			// 
			// pictureBox7
			// 
			this->pictureBox7->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox7.Image")));
			this->pictureBox7->Location = System::Drawing::Point(696, 389);
			this->pictureBox7->Name = L"pictureBox7";
			this->pictureBox7->Size = System::Drawing::Size(121, 62);
			this->pictureBox7->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox7->TabIndex = 9;
			this->pictureBox7->TabStop = false;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(667, 397);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(13, 13);
			this->label4->TabIndex = 10;
			this->label4->Text = L"д";
			// 
			// pictureBox10
			// 
			this->pictureBox10->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox10.Image")));
			this->pictureBox10->Location = System::Drawing::Point(696, 477);
			this->pictureBox10->Name = L"pictureBox10";
			this->pictureBox10->Size = System::Drawing::Size(121, 65);
			this->pictureBox10->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox10->TabIndex = 14;
			this->pictureBox10->TabStop = false;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(668, 477);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(13, 13);
			this->label5->TabIndex = 15;
			this->label5->Text = L"е";
			// 
			// helpl
			// 
			this->helpl->AutoSize = true;
			this->helpl->Location = System::Drawing::Point(693, 545);
			this->helpl->Name = L"helpl";
			this->helpl->Size = System::Drawing::Size(13, 13);
			this->helpl->TabIndex = 9;
			this->helpl->Text = L"0";
			this->helpl->Visible = false;
			// 
			// pictureBox4
			// 
			this->pictureBox4->Image = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"pictureBox4.Image")));
			this->pictureBox4->Location = System::Drawing::Point(696, 228);
			this->pictureBox4->Name = L"pictureBox4";
			this->pictureBox4->Size = System::Drawing::Size(120, 61);
			this->pictureBox4->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->pictureBox4->TabIndex = 16;
			this->pictureBox4->TabStop = false;
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(844, 637);
			this->Controls->Add(this->pictureBox4);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->pictureBox10);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->pictureBox7);
			this->Controls->Add(this->helpl);
			this->Controls->Add(this->pictureBox5);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->table);
			this->Controls->Add(this->groupBox1);
			this->Name = L"Form1";
			this->Text = L"Form1";
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox3))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox9))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox8))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox6))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox2))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->table))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox5))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox7))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox10))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox4))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	
private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		 //sozdanie file prostix chisel - ochen medlenno
	     /*std::ofstream file;
		 file.open("prost.txt");
		 unsigned long int n,i,j,k;
         n=100000000;
		 //file<<n;
		 for (i=2; i<=n; i++)
		 {
		     k=0; j=2;
			 while (k==0)
			 {
			   if (i%j==0) k=1;
			   if (j>i/2) k=2;
			   j++;
			 }
			 if (k==2)
		     file<<i<<"\n";
			 //for (j=2;j<=i/2;(i%j);j++);
            // if (j<=i/2 && !(i%j));
		 }
		 file.close();
		 MessageBox::Show("OK");
		 //---
		 */
			 //int64_t a;
			 label1->Text="Выполняется расчёт";
			 unsigned long long int kil;
			 int i;
			 long double t,pc,x,sum2,sum,sum1,xl2,c4=0.493091099368767,c5=0.150757555616266,c6=0.174762639299271;// sum dlya x^8
			 x=10;
			 t=0.333333333333333;
			 std::ifstream file;
		     file.open("prost.txt");
			 kil=0; pc=0; sum=0; sum1=0; i=Convert::ToInt32(helpl->Text); //(int)helpl->Text;
			 //file>>pc;
			 //table->Rows[0]->Cells[0]->Value = (pc).ToString();
			 while (x<=100000000)
			 {
				 table->Rows->Add();
				 while (!file.eof() && pc<x)
				 {
				   file>>pc;
				   //kil++;
				   if (pc<x)
				   {
					   
				   if (rb4->Checked) sum1+=logl(pc)/(pc*pc);
				    else 
				   if (rb5->Checked) sum1+=logl(pc)/(pc*pc*pc);
				    else
						if (rb6->Checked) sum1+=(1/(pc*pc*pc));
				    else 
					{
						sum2+=logl(pc);
						sum=sum2-x+sqrtl(x);
					}
					//sum+=logl(pc);

				   //label1->Text=(kil).ToString();
				   } //else kil++;
				 }
				 if (rb6->Checked) table->Rows[i]->Cells[0]->Value = (sum1).ToString();
				 else
				 if (rb5->Checked) table->Rows[i]->Cells[0]->Value = (sum1).ToString();
				 else
				 if (rb4->Checked) table->Rows[i]->Cells[0]->Value = (sum1).ToString();
				 else
				 table->Rows[i]->Cells[0]->Value = (sum).ToString();
				 kil++;
				 if (rb1->Checked) xl2=sqrtl(x); else
					 if (rb2->Checked) xl2=sqrtl(x)*logl(x); else
						if (rb3->Checked) 
						
							{
								
							//xl2=x-sqrtl(x)-(2.04715036378898*(logl(x)/sqrtl(x)));
								xl2=(-1)*(logl(x)/sqrtl(x));
								
						}
						
						else
							if (rb5->Checked) xl2=c5-(1/(x*x)); else
								if (rb6->Checked) xl2=c6-1/(2*x*x*logl(x)); else
								
							xl2=c4-(1/x);
							
				 table->Rows[i]->Cells[1]->Value = (xl2).ToString();
				 if (rb6->Checked) table->Rows[i]->Cells[2]->Value = (sum1/xl2).ToString();
				 else 
				 if (rb4->Checked) table->Rows[i]->Cells[2]->Value = (sum1/xl2).ToString();
				 else 
				 if (rb5->Checked) table->Rows[i]->Cells[2]->Value = (sum1/xl2).ToString();
				 else table->Rows[i]->Cells[2]->Value = (sum/xl2).ToString();
				 table->Rows[i]->Cells[3]->Value = (x).ToString();

				 if (rb4->Checked) sum1+=(logl(pc))/(pc*pc);
				 //logl(pc)/(pc*pc)
				 else 
                 if (rb5->Checked) sum1+=logl(pc)/(pc*pc*pc);
				 else
                 if (rb6->Checked) sum1+=(1/(pc*pc*pc));
				 else
				 {
						sum2+=logl(pc);
						sum=sum2-x+sqrtl(x);
					} 
					//sum+=logl(pc);

				 //table->Rows->Add();
				 i++;
				 x*=10;
			 }
			 file.close();
			 helpl->Text=i.ToString();
			 label1->Text="Готово";
			 //MessageBox::Show("OK");
			 
 		 } //button1Click

private: System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) {
			 table->Rows->Clear();
			 label1->Text="Статус";
		 }
private: System::Void pictureBox6_Click(System::Object^  sender, System::EventArgs^  e) {
		 }
private: System::Void rb5_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
		 }
private: System::Void pictureBox2_Click(System::Object^  sender, System::EventArgs^  e) {
		 }
private: System::Void pictureBox3_Click(System::Object^  sender, System::EventArgs^  e) {
		 }
private: System::Void pictureBox9_Click(System::Object^  sender, System::EventArgs^  e) {
		 }
};
}

