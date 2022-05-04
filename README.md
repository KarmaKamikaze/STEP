# STEP (Step Towards Effective Programming)

STEP is an imperative and type-safe programming language for Arduino.
It is beginner friendly and targets students in the range of 8 to 16 years.
It promotes structured programming, and supports simple single-run terminal programs and continuous Arduino programs.

<!-- GETTING STARTED -->
## Writing your first program

1. Familiarize yourself with the structure of a STEP program

An empty STEP program might look like this:
```
variables

end variables

setup

end setup

loop

end loop

functions

end functions
```
There are four main _scopes_. It is mandatory to provide either `setup` or `loop` - the rest are optional.

2. Understand the scopes

| Scope name      | Description                                                                         |
| --------------- | ----------------------------------------------------------------------------------- |
| variables       | Contains global variable declarations [^global]                                     |
| setup           | Contains code that should be run at initialization of program                       |
| loop            | Contains code that will run repeatedly until Arduino device is turned off           |
| functions       | Contains function definitions [^function]                                           |

[^global]: A global variable is accessible from anywhere in the program
[^function]: A procedure that can be executed from anywhere in the program

3. Understand the keywords, statements and control structures

STEP is formally documented in the [development journal](https://www.youtube.com/watch?v=dQw4w9WgXcQ). 

4. Hello, world!
```
setup
    Print("Hello, world!")
end setup
```
## Getting started

To get a local copy up and running, follow these steps.

### Dependencies

The project is dependant on the [Arduino-CLI](https://arduino.github.io/arduino-cli/0.21/installation/).

### Installation

1. Clone the repository

```sh
git clone https://github.com/KarmaKamikaze/STEP.git
```


2. Navigate to the root folder and create a new ArduinoCLI directory, then enter it

```sh
cd STEP/STEP/ && mkdir ArduinoCLI && cd ArduinoCLI/
```


3. Download the Arduino-CLI for your preferred operating system from this source and place it inside the new folder:

Linux:
```sh
wget https://downloads.arduino.cc/arduino-cli/arduino-cli_latest_Linux_64bit.tar.gz
```
Windows:
```sh
wget https://downloads.arduino.cc/arduino-cli/arduino-cli_latest_Windows_64bit.zip
```
macOS:
```sh
wget https://downloads.arduino.cc/arduino-cli/arduino-cli_latest_macOS_64bit.tar.gz
```


4. Extract the zipped folder to attain the arduino-cli executable

Linux:
```sh
tar –xvzf arduino-cli_latest_Linux_64bit.tar.gz
```
Windows:
```sh
tar –xvzf arduino-cli_latest_Windows_64bit.zip
```
macOS:
```sh
tar –xvzf arduino-cli_latest_macOS_64bit.tar.gz
```


5. Install the arduino avr-gcc compiler

```sh
./arduino-cli.exe core install arduino:avr
```


6. ??? Profit


## Using the compiler

1. Familiarize yourself with the compiler arguments

| Argument        | Description                                              |
| --------------- | -------------------------------------------------------- |
| -pp             | Prints the arduino code in the terminal                  |
| -upload [port]  | Uploads the arduino code to the arduino device [^port]      |
| -output [port]  | Displays arduino output in terminal after uploading [^port] |
| -ports          | Displays what devices are connected to which ports       |

[^port]: If no port is supplied, the compiler attempts to guess the correct port

2. Run the compiler

Linux & macOS:
```sh
./STEP [sourcefile] [args]
```
Windows:
```sh
./STEP.exe [sourcefile] [args]
``` 


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT --> 
## Contact

Project Link: [https://github.com/KarmaKamikaze/STEP](https://github.com/KarmaKamikaze/STEP)
