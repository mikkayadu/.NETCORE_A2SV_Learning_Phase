using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

public class Shape{
    public string name ;
    public Shape(string name) {
        this.name = name;
    }

    public virtual double CalculateArea(){
        return 1;
    }
}

class Circle : Shape{
    double radius;
    public Circle(string name, double radius):base(name){
        this.name = name;
        this .radius = radius;}

    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }



    }

public class Rectangle:Shape{
    double Width, Height;
    

    public Rectangle(string name , double Width, double Height):base(name){
        // this.name = name;
        this.Width = Width;
        this.Height = Height;
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}

public class Triangle:Shape{
    double Base; double Height;
    public Triangle(string name, double Base, double height):base(name){
        this.Base = Base;
        this.Height = height;
    }

    public override double CalculateArea()
    {
        return (Base * Height) / 2 ;
    }
}

class Task{
public static void PrintShapeArea(Shape shape){
    Console.WriteLine($"shape.Name: {shape.name}");
    Console.WriteLine($"Area: {Math.Round(shape.CalculateArea(), 3)}");
}
public static void Main(){
    Circle circle1 = new Circle("Circle1", 2);
    Rectangle rect1 = new Rectangle("Rectange1", 2, 2);
    Triangle triangle1 = new Triangle("Triangle1", 2.1, 2.2);

    Shape[] arr = new Shape[3] {circle1, rect1, triangle1};;

    for ( int i = 0; i<arr.Length; i++){
        PrintShapeArea(arr[i]);
    } 
}

}