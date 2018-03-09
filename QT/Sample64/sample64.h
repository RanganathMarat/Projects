#ifndef SAMPLE64_H
#define SAMPLE64_H

#include <QtWidgets/QMainWindow>
#include "ui_sample64.h"

class Sample64 : public QMainWindow
{
	Q_OBJECT

public:
	Sample64(QWidget *parent = 0);
	~Sample64();

private:
	Ui::Sample64Class ui;
};

#endif // SAMPLE64_H
