#ifndef EXAMCARDCONTROL_H
#define EXAMCARDCONTROL_H


#include <QFrame>
#include "rowcontrol.h"

namespace Ui {
class ExamCardControl;
}

class ExamCardControl : public QFrame
{
    Q_OBJECT

public:
    explicit ExamCardControl(QWidget *parent = 0);
    ~ExamCardControl();

private slots:
    void on_pushButton_clicked();

private:
    Ui::ExamCardControl *ui;
};

#endif // EXAMCARDCONTROL_H
