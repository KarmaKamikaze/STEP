const double num = 10.5;
String str = "test";
boolean boo = true;
void setup() {
pinMode(INPUT);

str = "test2";
double num2 = 1;
while(true) {
if(num2 > 20) {
break;
}
else if(num2 > num) {
num2 = num2 * 2;
continue;
}
else {
num2 = num2 + 1;
}
}
}
