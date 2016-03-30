#pragma once
#define _CRT_SECURE_NO_WARNINGS

#include <fstream>
#include "stdafx.h"
#include <Math.h>
#include <cstring>
#include <msclr\marshal_cppstd.h>
#include <set>
#include <map>
#include <list>
#include <iostream>
#include <string>
#include <algorithm>    // std::min
#include <numeric>   // std::iota
#include <ctime>

using namespace std;
namespace TestWords {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Сводка для Form1
	/// </summary>
	struct ValueStruct {
		int value = 0;
	};

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

	private: System::Windows::Forms::Button^  buttonOpenTxtFile;

	protected:









	private: System::Windows::Forms::TextBox^  textBoxWord;




	private: System::Windows::Forms::Label^  label2;






















	private: System::Windows::Forms::TextBox^  textBoxReadingTime;

	private: System::Windows::Forms::Label^  label9;
	private: System::Windows::Forms::TextBox^  textBoxWordsCount;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::TextBox^  textBoxXORKey;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::Button^  buttonRepeatAnalize;
	private: System::Windows::Forms::CheckBox^  checkBoxXORKey;











	private:



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
			this->buttonOpenTxtFile = (gcnew System::Windows::Forms::Button());
			this->textBoxWord = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->textBoxReadingTime = (gcnew System::Windows::Forms::TextBox());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->textBoxWordsCount = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->textBoxXORKey = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->buttonRepeatAnalize = (gcnew System::Windows::Forms::Button());
			this->checkBoxXORKey = (gcnew System::Windows::Forms::CheckBox());
			this->SuspendLayout();
			// 
			// buttonOpenTxtFile
			// 
			this->buttonOpenTxtFile->Location = System::Drawing::Point(12, 28);
			this->buttonOpenTxtFile->Name = L"buttonOpenTxtFile";
			this->buttonOpenTxtFile->Size = System::Drawing::Size(357, 42);
			this->buttonOpenTxtFile->TabIndex = 5;
			this->buttonOpenTxtFile->Text = L"Открыть и проанализировать файл";
			this->buttonOpenTxtFile->UseVisualStyleBackColor = true;
			this->buttonOpenTxtFile->Click += gcnew System::EventHandler(this, &Form1::button2_Click);
			// 
			// textBoxWord
			// 
			this->textBoxWord->Location = System::Drawing::Point(550, 26);
			this->textBoxWord->Name = L"textBoxWord";
			this->textBoxWord->Size = System::Drawing::Size(242, 20);
			this->textBoxWord->TabIndex = 12;
			this->textBoxWord->Text = L"аба";
			this->textBoxWord->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(417, 29);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(85, 13);
			this->label2->TabIndex = 10;
			this->label2->Text = L"Введите слово:";
			// 
			// textBoxReadingTime
			// 
			this->textBoxReadingTime->Location = System::Drawing::Point(550, 108);
			this->textBoxReadingTime->Name = L"textBoxReadingTime";
			this->textBoxReadingTime->RightToLeft = System::Windows::Forms::RightToLeft::Yes;
			this->textBoxReadingTime->Size = System::Drawing::Size(242, 20);
			this->textBoxReadingTime->TabIndex = 5;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(417, 111);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(88, 13);
			this->label9->TabIndex = 4;
			this->label9->Text = L"Время анализа:";
			// 
			// textBoxWordsCount
			// 
			this->textBoxWordsCount->Location = System::Drawing::Point(550, 134);
			this->textBoxWordsCount->Name = L"textBoxWordsCount";
			this->textBoxWordsCount->RightToLeft = System::Windows::Forms::RightToLeft::Yes;
			this->textBoxWordsCount->Size = System::Drawing::Size(242, 20);
			this->textBoxWordsCount->TabIndex = 14;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(417, 137);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(127, 13);
			this->label1->TabIndex = 13;
			this->label1->Text = L"Количество вхождений:";
			// 
			// textBoxXORKey
			// 
			this->textBoxXORKey->Location = System::Drawing::Point(550, 50);
			this->textBoxXORKey->Name = L"textBoxXORKey";
			this->textBoxXORKey->Size = System::Drawing::Size(242, 20);
			this->textBoxXORKey->TabIndex = 16;
			this->textBoxXORKey->Text = L"0123456789ABCDEF0123456789ABCDEF";
			this->textBoxXORKey->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(417, 53);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(97, 13);
			this->label3->TabIndex = 15;
			this->label3->Text = L"Введите xor-ключ:";
			// 
			// buttonRepeatAnalize
			// 
			this->buttonRepeatAnalize->Enabled = false;
			this->buttonRepeatAnalize->Location = System::Drawing::Point(12, 76);
			this->buttonRepeatAnalize->Name = L"buttonRepeatAnalize";
			this->buttonRepeatAnalize->Size = System::Drawing::Size(357, 42);
			this->buttonRepeatAnalize->TabIndex = 17;
			this->buttonRepeatAnalize->Text = L"Повторный запуск без открытия файла";
			this->buttonRepeatAnalize->UseVisualStyleBackColor = true;
			this->buttonRepeatAnalize->Click += gcnew System::EventHandler(this, &Form1::buttonRepeatAnalize_Click);
			// 
			// checkBoxXORKey
			// 
			this->checkBoxXORKey->AutoSize = true;
			this->checkBoxXORKey->Location = System::Drawing::Point(550, 76);
			this->checkBoxXORKey->Name = L"checkBoxXORKey";
			this->checkBoxXORKey->Size = System::Drawing::Size(83, 17);
			this->checkBoxXORKey->TabIndex = 18;
			this->checkBoxXORKey->Text = L"Применить";
			this->checkBoxXORKey->UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::SystemColors::Window;
			this->ClientSize = System::Drawing::Size(864, 211);
			this->Controls->Add(this->checkBoxXORKey);
			this->Controls->Add(this->buttonRepeatAnalize);
			this->Controls->Add(this->textBoxXORKey);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->textBoxWordsCount);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->textBoxReadingTime);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->textBoxWord);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->buttonOpenTxtFile);
			this->Name = L"Form1";
			this->Text = L"Поиск слова в файле";
			this->ResumeLayout(false);
			this->PerformLayout();

		}


#pragma endregion

		int wordsCount;
		OpenFileDialog^ openFileDialog;

		protected: void ReadTextFile(std::string path)
		{
			FileReader 	sfText;
			bool		bReturn = false;

			std::string	szLine;
			wordsCount = 0;
			
			msclr::interop::marshal_context context;

			sfText.word = context.marshal_as<std::string>( textBoxWord->Text );
			sfText.key = context.marshal_as<std::string>( textBoxXORKey->Text );

			if ( sfText.Open( path ) )
			{
				typedef void (FileReader::*SomeClassMFP)();
				SomeClassMFP my_memfunc_ptr;

				if (checkBoxXORKey->Checked) {

					my_memfunc_ptr = &FileReader::FilterWithXOR;
				}
				else {

					my_memfunc_ptr = &FileReader::FilterWithoutXOR;
				}

				while ( sfText.GetNextLine() != 0 )
				{
					(sfText.*my_memfunc_ptr)();
				}

				sfText.Close();
			}// pass

			textBoxWordsCount->Text = gcnew String( sfText.m_wordsCount.ToString() );
		}

		protected:  System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) {
			Stream^ myStream;
			openFileDialog = gcnew OpenFileDialog;

			openFileDialog->Filter = "Выберите файл(*.txt)|*.txt";
			openFileDialog->FilterIndex = 2;
			openFileDialog->RestoreDirectory = true;

			if (openFileDialog->ShowDialog() == System::Windows::Forms::DialogResult::OK)
			{
				if ((myStream = openFileDialog->OpenFile()) != nullptr)
				{
					msclr::interop::marshal_context context;

					string currentFileName = context.marshal_as<std::string>(openFileDialog->FileName);
					
					unsigned int start_time = clock(); // начальное время

					ReadTextFile( currentFileName);
					
					unsigned int end_time = clock(); // конечное время
					unsigned int search_time = end_time - start_time; // искомое время

					textBoxReadingTime->Text = gcnew String(search_time.ToString());

					buttonRepeatAnalize->Enabled = true;
				}

				myStream->Close();
			}
		}

		protected: System::Void buttonRepeatAnalize_Click(System::Object^  sender, System::EventArgs^  e) {

			msclr::interop::marshal_context context;

			string currentFileName = context.marshal_as<std::string>(openFileDialog->FileName);

			unsigned int start_time = clock(); // начальное время

			ReadTextFile(currentFileName);

			unsigned int end_time = clock(); // конечное время
			unsigned int search_time = end_time - start_time; // искомое время

			textBoxReadingTime->Text = gcnew String(search_time.ToString());
		}
};
}

