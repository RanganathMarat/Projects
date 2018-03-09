#include "sample.h"
#include <QtWidgets/QFileSystemModel>

Sample::Sample(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);

	//setWindowFlags(Qt::MSWindowsFixedSizeDialogHint); //Set window to fixed size
	//setWindowFlags(Qt::CustomizeWindowHint); //Set window with no title bar
	//setWindowFlags(Qt::FramelessWindowHint); //Set a frameless window
	
	connect(ui.pushButton, &QAbstractButton::clicked, this, &Sample::OnClickingPushButton);
}

Sample::~Sample()
{

}


void Sample::OnClickingPushButton()
{
	close();
}