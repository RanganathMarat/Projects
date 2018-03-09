#include "sample64.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	Sample64 w;
	w.show();
	return a.exec();
}
