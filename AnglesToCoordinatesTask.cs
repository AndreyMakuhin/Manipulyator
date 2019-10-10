using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbow_new = elbow - (Math.PI - shoulder);
            var wrist_new = wrist - (Math.PI - shoulder) - (Math.PI - elbow);

            var elbowPos = new PointF((float)(Manipulator.UpperArm * Math.Cos(shoulder)),
                                      (float)(Manipulator.UpperArm * Math.Sin(shoulder)));
            var wristPos = new PointF((float)(elbowPos.X + Manipulator.Forearm * Math.Cos(elbow_new)),
                                      (float)(elbowPos.Y + Manipulator.UpperArm * Math.Sin(elbow_new)));
            var palmEndPos = new PointF((float)(wristPos.X + Manipulator.Palm * Math.Cos(wrist_new)),
                                        (float)(wristPos.Y + Manipulator.Palm * Math.Sin(wrist_new)));
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }
    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }

    }

}