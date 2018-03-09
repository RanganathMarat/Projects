#ifndef SAMPLECONTROL_H
#define SAMPLECONTROL_H

#include <QFrame>

namespace Ui {
class SampleControl;
}

class SampleControl : public QFrame
{
    Q_OBJECT

public:
    explicit SampleControl(QWidget *parent = 0);
    ~SampleControl();

private:
    Ui::SampleControl *ui;
};

#endif // SAMPLECONTROL_H
