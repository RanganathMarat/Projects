import QtQuick 1.1

Item {
    id: textBox
    width: 200
    height: 20
    property alias text: textInput.text

    Rectangle {
        id: textLayout
        width: textBox.width
        height: textBox.height
        color: "black"
        radius: 2
        gradient: Gradient {
            GradientStop {
                position: 0.00;
                color: "#000000";
            }
            GradientStop {
                position: 0.92;
                color: "#ffffff";
            }
        }
        smooth: true

        TextInput {
            id: textInput
            font.bold: true
            anchors.fill: parent
            font.pointSize: 14
            horizontalAlignment: TextInput.AlignLeft
        }
    }
}
