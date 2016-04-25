//
//  w32st02.c d/asinxronnogo cluchaja
//  demo d/oprosa ADAM-4080D
//  proveril rabotu COM1 na kom.1 pod WINDOWS XP
//

#include <windows.h>
#include <string.h>
#include<stdio.h>
#include<time.h>
#include<stdlib.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
int UpdteFile(void);
//imja klassa okna
char szProgName[]="w32st00";
char szApplName[]="vmk.ksait";
char strk[80]="";
char stro[80]="";
char stri[80]="";
char pristr[540]="";
char perstr[40]="";
char inBuff[1040],chv;
int ia=0,ib=0;
int X=1,Y=1;
int nChWa,flgWa;
LPSTR lpTextBuffer;
char szDatup[140];
char szUprds[120];
char szUprdt[120];
int  dwUprf[120];
OFSTRUCT ofs;
HANDLE hfSrcFile;
LPSTR lpUroven[1];
char szUroven1[]="opfi.txt";
HANDLE hCom;
DWORD dwKolo,dwKoli,dwFileSize;
DWORD dwError;
DCB dcb;
COMSTAT cmst;
COMMTIMEOUTS ctmo,*pctmo;
#define READ_TIMEOUT 2000
DWORD dwRead,dwRes;
OVERLAPPED ovl3={0};
BOOL fSuccess;
MSG  lpMsg;
char szMsg[60]="";
int ima0,i;
union{
	  char bytm[4];
	  DWORD dwtm;}tmo00;
int WINAPI WinMain(HINSTANCE hInst,HINSTANCE hPreInst,
                   LPSTR lpszCmdLine,int nCmdShow)
{
  HWND hWnd;
  MSG  lpMsg;
  WNDCLASS wcApp;
  char szMsg[60]="";
  HDC hnc;
         lpUroven[0]=szUroven1;
         for(ima0=0;ima0<120;ima0++) {
			 szUprds[ima0]=0x30; dwUprf[ima0]=0;}
		 for(ima0=45;ima0<62;ima0++)
			 szUprds[ima0]=0x45;
		 for(ima0=0;ima0<140;ima0++) szDatup[ima0]=0x20;
  hCom=CreateFile("COM2",GENERIC_READ|GENERIC_WRITE,
         0,NULL,OPEN_EXISTING,FILE_FLAG_OVERLAPPED,NULL);
         if(hCom==INVALID_HANDLE_VALUE){
           dwError=GetLastError();
   wsprintf(szMsg,"Error N=%d open Port\n Result= %d",dwError,hCom);
       MessageBox(NULL,szMsg,"Initializ.",MB_OK|MB_ICONEXCLAMATION);
          return FALSE;                 }
         PurgeComm(hCom,PURGE_TXCLEAR|PURGE_RXCLEAR);
         fSuccess=BuildCommDCB("COM2:9600,n,8,1",&dcb);
         if(!fSuccess){
          dwError=GetLastError();
  MessageBox(NULL,"Error move DCB","Initializ.",MB_OK|MB_ICONEXCLAMATION);
         return FALSE;
        }
         fSuccess=SetCommState(hCom,&dcb);
         if(!fSuccess){
    MessageBox(NULL,"Error setup","Initializ.",MB_OK|MB_ICONEXCLAMATION);
         return FALSE;
        } 
		  pctmo=&ctmo;
		 ctmo.ReadIntervalTimeout=100;
		 ctmo.ReadTotalTimeoutConstant=1000;
		 ctmo.ReadTotalTimeoutMultiplier=200;
		 ctmo.WriteTotalTimeoutConstant=1000;
		 ctmo.WriteTotalTimeoutMultiplier=200;
		 SetCommTimeouts(hCom,pctmo);
		 GetCommTimeouts(hCom,pctmo);
		 tmo00.dwtm=ctmo.ReadIntervalTimeout;
		 
  wcApp.lpszClassName=szProgName;
  wcApp.hInstance    =hInst;
  wcApp.lpfnWndProc  =WndProc;
  wcApp.hCursor      =LoadCursor(NULL,IDC_ARROW);
  wcApp.hIcon        =NULL;
  wcApp.lpszMenuName =szApplName;
  wcApp.hbrBackground=GetStockObject(WHITE_BRUSH);
  wcApp.style        =CS_HREDRAW|CS_VREDRAW;
  wcApp.cbClsExtra   =0;
  wcApp.cbWndExtra   =0;
  if (!RegisterClass (&wcApp))
    return 0;

  hWnd=CreateWindow(szProgName,"Programma d/studentov",
                    WS_OVERLAPPEDWINDOW,CW_USEDEFAULT,
                    CW_USEDEFAULT,250,60,(HWND)NULL,
                    (HMENU)NULL,(HANDLE)hInst,(LPSTR)NULL);
  ShowWindow(hWnd,nCmdShow);
  UpdateWindow(hWnd);
  hnc=GetDC(hWnd);
   strcpy(strk,"Priem dannix");
   TextOut(hnc,1,1,strk,strlen(strk));
   ReleaseDC(hWnd,hnc);
   SetTimer(hWnd,1,5000,NULL);
  while (GetMessage(&lpMsg,NULL,0,0)) {
    TranslateMessage(&lpMsg);
    DispatchMessage(&lpMsg);
  }
   KillTimer(hWnd,1);
  return(lpMsg.wParam);
}

LONG WINAPI WndProc(HWND hWnd,UINT messg,
                    UINT wParam,LONG lParam)
{
  HDC hdc;
  PAINTSTRUCT ps;
  struct tm *newtime;
  time_t t;
  
  switch (messg)
  {
    case WM_TIMER:
    {
       stro[0]=0x41;
       stro[1]=0;
          t=time(NULL);
          newtime=localtime(&t);
          strcat(stro,asctime(newtime));
		  stro[strlen(stro)-1]='\0';
          for(i=0;i<9;i++) pristr[i]=szDatup[i+8];
		  pristr[9]=0;
		  strcat(stro,pristr);
          stro[strlen(stro)-1]='\0';
         PurgeComm(hCom,PURGE_TXCLEAR|PURGE_RXCLEAR);
         X=1;
         Y=1;
		 perstr[0]='#';
		 perstr[1]='0';
		 perstr[2]='2';
         perstr[3]='1';
		 perstr[4]=13;
		 perstr[5]=0;
		 
         ovl3.hEvent=CreateEvent(NULL,TRUE,FALSE,NULL);
         if(!WriteFile(hCom,&perstr,5,&dwKolo,&ovl3))
		 {
			 if(GetLastError() != ERROR_IO_PENDING)
	 MessageBox(NULL,szMsg,"osh05?",MB_OK|MB_ICONEXCLAMATION);
			 else
			 {
			   dwRes=WaitForSingleObject(ovl3.hEvent,READ_TIMEOUT);
               switch(dwRes)
			   {
			    case WAIT_OBJECT_0:
                 GetOverlappedResult(hCom,&ovl3,&dwKolo,FALSE);
     //MessageBox(NULL,szMsg,"osh03?",MB_OK|MB_ICONEXCLAMATION);
	            break;
				case WAIT_TIMEOUT:
     MessageBox(NULL,szMsg,"osh04?",MB_OK|MB_ICONEXCLAMATION);
                break;
			   }
			   ResetEvent(ovl3.hEvent);
			 }
		 }
		 else
         {
          ResetEvent(ovl3.hEvent);
		 }
		 CloseHandle(ovl3.hEvent);
		 ovl3.hEvent=CreateEvent(NULL,TRUE,FALSE,NULL);
         if(!ReadFile(hCom,&pristr,82,&dwKoli,&ovl3))
		 {
			 if(GetLastError() != ERROR_IO_PENDING)
	 MessageBox(NULL,szMsg,"osh00?",MB_OK|MB_ICONEXCLAMATION);
			 else
			 {
			   dwRes=WaitForSingleObject(ovl3.hEvent,READ_TIMEOUT);
               switch(dwRes)
			   {
			    case WAIT_OBJECT_0:
                 GetOverlappedResult(hCom,&ovl3,&dwKoli,FALSE);
     //MessageBox(NULL,szMsg,"osh01?",MB_OK|MB_ICONEXCLAMATION);
	      tmo00.dwtm=dwKolo;
		  for(i=0;i<4;i++) szDatup[i]=tmo00.bytm[i];
		  tmo00.dwtm=dwKoli;
		  for(i=0;i<4;i++) szDatup[i+4]=tmo00.bytm[i];
		  for(i=0;i<10;i++) szDatup[i+8]=pristr[i];
		        break;
				case WAIT_TIMEOUT:
     MessageBox(NULL,szMsg,"osh02?",MB_OK|MB_ICONEXCLAMATION);
                break;
			   }
			   ResetEvent(ovl3.hEvent);
			 }
		 }
		 else
         {
          tmo00.dwtm=dwKolo;
		  for(i=0;i<4;i++) szDatup[i]=tmo00.bytm[i];
		  tmo00.dwtm=dwKoli;
		  for(i=0;i<4;i++) szDatup[i+4]=tmo00.bytm[i];
		  for(i=0;i<10;i++) szDatup[i+8]=pristr[i];
		  ResetEvent(ovl3.hEvent);
		 }
		 CloseHandle(ovl3.hEvent);
           InvalidateRect(hWnd,NULL,1);
           UpdateWindow(hWnd);
      return 0;
    }
    case WM_PAINT:
      hdc=BeginPaint(hWnd,&ps);
      TextOut(hdc,X,Y,stro,strlen(stro));
      ValidateRect(hWnd,NULL);
      EndPaint(hWnd,&ps);
      break;

    case WM_DESTROY:
      CloseHandle(hCom);
      PostQuitMessage(0);
      break;

    default:
      return(DefWindowProc(hWnd,messg,wParam,lParam));
  }
  return(0);
}
