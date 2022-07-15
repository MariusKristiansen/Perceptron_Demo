using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron;
public class Point
{
    public double x;
    public double y;
    public Label Label;
    private LabelAssignmentDelegate AssignLabel;
    public delegate Label LabelAssignmentDelegate(double x, double y);
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
        AssignLabel = (double x, double y) => x > y ? Label.ACTIVE : Label.INACTIVE;
        Label = AssignLabel(x, y);
    }

    public static implicit operator Point(double[] values)
    {
        return new Point(values[0], values[1]);
    }

    public static implicit operator double[](Point point)
    {
        return new double[] { point.x, point.y };
    }

    public static implicit operator bool(Point point)
    {
        return (int)point.Label == 0;
    }

    public double State => (int)Label == 0 ? 1 : -1;
    

    public void ReassignLabel()
    {
        Label = AssignLabel(x, y);
    }

    public void AssignLabelDelegate(LabelAssignmentDelegate assignLabel)
    {
        AssignLabel = assignLabel;
    }
}

public enum Label
{
    ACTIVE, INACTIVE
}