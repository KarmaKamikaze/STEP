#define apin 3
#define dpin 1
// Global variables
const double num = 10.5;
String str = "test";
boolean boo = true;

// Functions
void blankfunc(String array[]) {
    String* tempArray = (String*)malloc(3 * sizeof(String));
    String __temp0__[] = {"test1", "test2", "test3"};
    memcpy(tempArray, __temp0__, sizeof(__temp0__[0])*3);
    free(tempArray);
}

String stringfunc(double num, boolean isConstant) {
    String* arr = (String*)malloc(3 * sizeof(String));
    String __temp1__[] = {"t1", "t2", "t3"};
    memcpy(arr, __temp1__, sizeof(__temp1__[0])*3);
    blankfunc(arr);
    if(isConstant || true && false) {
        return "constant " + String(num);
    }
    else {
        return "not constant " + String(num);
    }
    return "fail";
    free(arr);
}

void setup() {
    Serial.begin(9600);
    pinMode(1, INPUT);
    pinMode(A3, OUTPUT);

    str = "test2";
    double num2 = 0;
    for(double i = num2; i <= num; i = i + 3) {
        num2 = num2 + 1 * 1 + (2 + -2);
    }
}
void loop() {
    double num3 = 1;
    while(true) {
        if(num3 > 2 * 10 + (15 - 15)) {
            break;
        }
        else if(num3 > num || num3 == num) {
            num3 = num3 * 2;
            continue;
        }
        else {
            num3 = num3 + 1;
        }
    }
    str = stringfunc(num3, boo);
}
