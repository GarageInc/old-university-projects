#pragma once
#include "PolarPoint.h"
#include <math.h>

namespace Polar {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Сводка для MainForm
	/// </summary>
	public ref class MainForm : public System::Windows::Forms::Form
	{
	public:
		MainForm(void)
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
		~MainForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::GroupBox^  groupBoxIn;
	private: System::Windows::Forms::TextBox^  textBoxIn;
	protected:

	private: System::Windows::Forms::TextBox^  textBoxInFile;

	private: System::Windows::Forms::RadioButton^  radioButtonInFile;

	private: System::Windows::Forms::RadioButton^  radioButtonIn;

	private: System::Windows::Forms::GroupBox^  groupBoxOut;
	private: System::Windows::Forms::TextBox^  textBoxOut;

	private: System::Windows::Forms::TextBox^  textBoxOutFile;

	private: System::Windows::Forms::RadioButton^  radioButtonOutFile;

	private: System::Windows::Forms::RadioButton^  radioButtonOut;
	private: System::Windows::Forms::Button^  buttonProcess;



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
			this->groupBoxIn = (gcnew System::Windows::Forms::GroupBox());
			this->groupBoxOut = (gcnew System::Windows::Forms::GroupBox());
			this->radioButtonIn = (gcnew System::Windows::Forms::RadioButton());
			this->radioButtonInFile = (gcnew System::Windows::Forms::RadioButton());
			this->radioButtonOut = (gcnew System::Windows::Forms::RadioButton());
			this->radioButtonOutFile = (gcnew System::Windows::Forms::RadioButton());
			this->textBoxInFile = (gcnew System::Windows::Forms::TextBox());
			this->textBoxOutFile = (gcnew System::Windows::Forms::TextBox());
			this->textBoxIn = (gcnew System::Windows::Forms::TextBox());
			this->textBoxOut = (gcnew System::Windows::Forms::TextBox());
			this->buttonProcess = (gcnew System::Windows::Forms::Button());
			this->groupBoxIn->SuspendLayout();
			this->groupBoxOut->SuspendLayout();
			this->SuspendLayout();
			// 
			// groupBoxIn
			// 
			this->groupBoxIn->Controls->Add(this->textBoxIn);
			this->groupBoxIn->Controls->Add(this->textBoxInFile);
			this->groupBoxIn->Controls->Add(this->radioButtonInFile);
			this->groupBoxIn->Controls->Add(this->radioButtonIn);
			this->groupBoxIn->Location = System::Drawing::Point(13, 13);
			this->groupBoxIn->Name = L"groupBoxIn";
			this->groupBoxIn->Size = System::Drawing::Size(290, 335);
			this->groupBoxIn->TabIndex = 0;
			this->groupBoxIn->TabStop = false;
			this->groupBoxIn->Text = L"Input";
			// 
			// groupBoxOut
			// 
			this->groupBoxOut->Controls->Add(this->textBoxOut);
			this->groupBoxOut->Controls->Add(this->textBoxOutFile);
			this->groupBoxOut->Controls->Add(this->radioButtonOutFile);
			this->groupBoxOut->Controls->Add(this->radioButtonOut);
			this->groupBoxOut->Location = System::Drawing::Point(310, 13);
			this->groupBoxOut->Name = L"groupBoxOut";
			this->groupBoxOut->Size = System::Drawing::Size(290, 335);
			this->groupBoxOut->TabIndex = 0;
			this->groupBoxOut->TabStop = false;
			this->groupBoxOut->Text = L"Output";
			// 
			// radioButtonIn
			// 
			this->radioButtonIn->AutoSize = true;
			this->radioButtonIn->Location = System::Drawing::Point(6, 19);
			this->radioButtonIn->Name = L"radioButtonIn";
			this->radioButtonIn->Size = System::Drawing::Size(75, 17);
			this->radioButtonIn->TabIndex = 0;
			this->radioButtonIn->TabStop = true;
			this->radioButtonIn->Text = L"From here:";
			this->radioButtonIn->UseVisualStyleBackColor = true;
			// 
			// radioButtonInFile
			// 
			this->radioButtonInFile->AutoSize = true;
			this->radioButtonInFile->Location = System::Drawing::Point(6, 286);
			this->radioButtonInFile->Name = L"radioButtonInFile";
			this->radioButtonInFile->Size = System::Drawing::Size(67, 17);
			this->radioButtonInFile->TabIndex = 1;
			this->radioButtonInFile->TabStop = true;
			this->radioButtonInFile->Text = L"From file:\r\n";
			this->radioButtonInFile->UseVisualStyleBackColor = true;
			// 
			// radioButtonOut
			// 
			this->radioButtonOut->AutoSize = true;
			this->radioButtonOut->Location = System::Drawing::Point(7, 19);
			this->radioButtonOut->Name = L"radioButtonOut";
			this->radioButtonOut->Size = System::Drawing::Size(51, 17);
			this->radioButtonOut->TabIndex = 0;
			this->radioButtonOut->TabStop = true;
			this->radioButtonOut->Text = L"Here:";
			this->radioButtonOut->UseVisualStyleBackColor = true;
			// 
			// radioButtonOutFile
			// 
			this->radioButtonOutFile->AutoSize = true;
			this->radioButtonOutFile->Location = System::Drawing::Point(7, 285);
			this->radioButtonOutFile->Name = L"radioButtonOutFile";
			this->radioButtonOutFile->Size = System::Drawing::Size(57, 17);
			this->radioButtonOutFile->TabIndex = 1;
			this->radioButtonOutFile->TabStop = true;
			this->radioButtonOutFile->Text = L"To file:";
			this->radioButtonOutFile->UseVisualStyleBackColor = true;
			// 
			// textBoxInFile
			// 
			this->textBoxInFile->Location = System::Drawing::Point(7, 309);
			this->textBoxInFile->Name = L"textBoxInFile";
			this->textBoxInFile->Size = System::Drawing::Size(277, 20);
			this->textBoxInFile->TabIndex = 2;
			// 
			// textBoxOutFile
			// 
			this->textBoxOutFile->Location = System::Drawing::Point(7, 308);
			this->textBoxOutFile->Name = L"textBoxOutFile";
			this->textBoxOutFile->Size = System::Drawing::Size(277, 20);
			this->textBoxOutFile->TabIndex = 2;
			// 
			// textBoxIn
			// 
			this->textBoxIn->Location = System::Drawing::Point(7, 42);
			this->textBoxIn->Multiline = true;
			this->textBoxIn->Name = L"textBoxIn";
			this->textBoxIn->Size = System::Drawing::Size(277, 238);
			this->textBoxIn->TabIndex = 3;
			// 
			// textBoxOut
			// 
			this->textBoxOut->Location = System::Drawing::Point(7, 43);
			this->textBoxOut->Multiline = true;
			this->textBoxOut->Name = L"textBoxOut";
			this->textBoxOut->Size = System::Drawing::Size(277, 236);
			this->textBoxOut->TabIndex = 3;
			// 
			// buttonProcess
			// 
			this->buttonProcess->Location = System::Drawing::Point(13, 355);
			this->buttonProcess->Name = L"buttonProcess";
			this->buttonProcess->Size = System::Drawing::Size(587, 23);
			this->buttonProcess->TabIndex = 1;
			this->buttonProcess->Text = L"PROCESS";
			this->buttonProcess->UseVisualStyleBackColor = true;
			this->buttonProcess->Click += gcnew System::EventHandler(this, &MainForm::buttonProcess_Click);
			// 
			// MainForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(611, 400);
			this->Controls->Add(this->buttonProcess);
			this->Controls->Add(this->groupBoxOut);
			this->Controls->Add(this->groupBoxIn);
			this->Name = L"MainForm";
			this->Text = L"MainForm";
			this->groupBoxIn->ResumeLayout(false);
			this->groupBoxIn->PerformLayout();
			this->groupBoxOut->ResumeLayout(false);
			this->groupBoxOut->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void buttonProcess_Click(System::Object^  sender, System::EventArgs^  e) {
				 String^ input;
				 String^ output = "";
				 if (radioButtonIn->Checked) //ввод с TextEdit
					 input = textBoxIn->Text;
				 else if (radioButtonInFile->Checked) //ввод из файла
				 {
					 String^ filename = textBoxInFile->Text;
					 StreamReader^ in = File::OpenText(filename);
					 input = in->ReadToEnd();
					 in->Close();
				 }
				 else
				 {
					 MessageBox::Show("You must select one of the options.", "Error",
						 MessageBoxButtons::OK, MessageBoxIcon::Error);
					 return;
				 }
				 try
				 {
					 array<String^>^ pts = input->Split((gcnew String("\n"))->ToCharArray());
					 System::Collections::Generic::List<PolarPoint^>^ ppoints
						 = gcnew System::Collections::Generic::List<PolarPoint^>();
					 for (int i = 0; i < pts->Length; i++)
					 {
						 array<String^>^ numbers = pts[i]->Split((gcnew String(";"))->ToCharArray());
						 double radius = System::Double::Parse(numbers[0]);
						 double angle = System::Double::Parse(numbers[1]);
						 ppoints->Add(gcnew PolarPoint(radius, angle));
					 }
					 for (int i = 0; i < ppoints->Count; i += 3)
					 {
						 //берем 3 точки
						 PolarPoint^ p1 = ppoints[i];
						 PolarPoint^ p2 = ppoints[i + 1];
						 PolarPoint^ p3 = ppoints[i + 2];
						 double a = p1->distance(p2);
						 double b = p1->distance(p3);
						 double c = p2->distance(p3);
						 double p = (a + b + c)*0.5;
						 double s = sqrt(p*(p - a)*(p - b)*(p - c)); //формула Герона
						 output += s.ToString();
					 }
					 if (radioButtonOut->Checked) //вывод в TextEdit
						 textBoxOut->Text = output;
					 else if (radioButtonOutFile->Checked) //ввод из файла
					 {
						 String^ filename = textBoxOutFile->Text;
						 StreamWriter^ out = File::CreateText(filename);
						 out->Write(output);
						 out->Close();
					 }
					 else
					 {
						 MessageBox::Show("You must select one of the options.", "Error",
							 MessageBoxButtons::OK, MessageBoxIcon::Error);
						 return;
					 }
				 }
				 catch (System::Exception^ ex)
				 {
					 MessageBox::Show(ex->ToString(), "Error",
						 MessageBoxButtons::OK, MessageBoxIcon::Error);
				 }
	}
	};
}
