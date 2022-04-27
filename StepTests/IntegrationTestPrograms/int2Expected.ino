const double num = 10.5;
String str = "test";
boolean boo = true;
void setup() {
pinMode(INPUT);

str = "test2";
double num2 = 0;
for(double i = num2; i <= num; i = i + 3) {
num2 = num2 + 1;
}
}
void loop() {
double num3 = 1;
while(true) {
if(num3 > 20) {
break;
}
else if(num3 > num) {
num3 = num3 * 2;
continue;
}
else {
num3 = num3 + 1;
}
}
}
