# DesignProject
This is a student project, please do not use it for a professional practice.

This is something I made as a structural engineering student. I found producing a design report from design calculations is quite time consuming. So, I decided to explore possibilities of automating that process through programming.

In order to achieve this, I created a container class called "item", which has numerical value, unit, and description. I also created classes which conduct different structural calculations using the items as input parameters. The classes had item lists, to which input variable to the calculation, the steps of the calculation, and the results from the calculation are stored. These lists can be displayed as a table, exported directly to Microsoft Excel and Word.

I developed this program as I working on a structural design project, I did not make this other users in mind. Therefore, it is missing graphical user interface. This was my exploration effort to find the way to utilize my programming in structural engineering design, because of that, this program is incomplete. 

I tried to overcome this shortcoming, the lack of graphical user interface, through tried to create 2D and 3D interface. The 2D interface uses .NET library, it can handle elements through database, and element can be selected by clicking them on the image. Producing drawings for different structural elements would be a huge task, which cannot be done without a team of programmers. Same can be said for 3D interface. The 3D interface uses modern OpenGL and there is quite long way to go to be usable. The possibility of creating AutoCAD drawing from the program was explored, which would be very useful, it would be very time consuming process.
