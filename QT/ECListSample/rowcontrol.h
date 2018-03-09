#ifndef ROWCONTROL_H
#define ROWCONTROL_H

#include <QFrame>
#include <QtGlobal>
namespace Ui {
class RowControl;
}

class RowControl : public QFrame
{
    Q_OBJECT

public:
    explicit RowControl(QWidget *parent = 0);
    ~RowControl();

public slots:
    void SetInteractor(int i);

private:
    Ui::RowControl *ui;
};

#endif // ROWCONTROL_H
