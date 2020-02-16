/************************************************************ 
;   Backpropagation with momentum                           * 
;   by Andres Perez-Uribe                                   *
;   Universidad del Valle, Cali, Colombia                   *
;   sep/93                                                  *
;                                                           *
;   Email : aperez@lslsun.epfl.ch                           * 
;           Logic Systems Laboratory                        * 
;           Swiss Federal Institute of Technology-Lausanne  *
;           http://lslwww.epfl.ch/~aperez/                  *
;************************************************************

   References :
   -  G. Hinton, "How neural networks learn from experience",
      Scientific American, sep 1992.
   -  P. Werbos,  "The Roots of Backpropagation: From ordered derivatives 
      to Neural Neworks and Political Forecasting", John Wiley and Sons, 
      New York, 1994

   Compile : gcc -o Bkprop Bkprop.c -lm
   Run     : see example at the end of the C code. 

    There is no guarantee that the code will do what you
    expect or that it is error free. It is simply meant
    to provide a useful way to experiment with the
    Backpropagation learning algorithm.

    Last Update Oct 7/99...thanks to Stephane Pouyet <pouyet@nist.gov>
*/

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#define sigm(x)    1/(1 + exp(-(double)x))
#define dxsigm(y)  (float)(y)*(1.0-y))
#define IN         35    /* number if inputs */
#define HIDDEN     5     /* number of hidden units */
#define OUT        10    /* number of outputs */
#define EPSILON    0.05  /* maximum Mean Square Error to stop training */
#define NUMTRAIN   18    /* number of training patterns */

float inhiddw[IN][HIDDEN];
float hidoutw[HIDDEN][OUT];
float deltaihw[IN][HIDDEN];
float deltahow[HIDDEN][OUT];
float x[IN];
float y[HIDDEN];
float z[OUT];

/* training patterns */
int   actafer[NUMTRAIN][IN] = { { 0,1,1,1,1,1,0,             
				  1,0,0,0,0,0,1,
				  1,0,0,0,0,0,1,
				  1,0,0,0,0,0,1,
				  0,1,1,1,1,1,0 },

				{ 0,0,0,0,0,0,0,
				  0,1,0,0,0,0,1,
				  1,1,1,1,1,1,1,
				  0,0,0,0,0,0,1,
				  0,0,0,0,0,0,0 },

				{ 0,1,0,0,0,0,1,
				  1,0,0,0,0,1,1,
				  1,0,0,0,1,0,1,
				  1,0,0,1,0,0,1,
				  0,1,1,0,0,0,1 },

				{ 1,0,0,0,0,1,0,
				  1,0,0,0,0,0,1,
				  1,0,0,1,0,0,1,
				  1,1,1,0,1,0,1,
				  1,0,0,0,1,1,0 },

				{ 0,0,0,1,1,0,0,
				  0,0,1,0,1,0,0,
				  0,1,0,0,1,0,0,
				  1,1,1,1,1,1,1,
				  0,0,0,0,1,0,0 },

				{ 1,1,1,0,0,1,0,
				  1,0,1,0,0,0,1,
				  1,0,1,0,0,0,1,
				  1,0,1,0,0,0,1,
				  1,0,0,1,1,1,0 },
				       
				{ 0,0,1,1,1,1,0,
				  0,1,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  0,0,0,0,1,1,0 },

				{ 1,0,0,0,0,0,0,             
				  1,0,0,0,0,0,0,
				  1,0,0,1,1,1,1,
				  1,0,1,0,0,0,0,
				  1,1,0,0,0,0,0 },

				{ 0,1,1,0,1,1,0,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  0,1,1,0,1,1,0 },

				{ 0,1,1,0,0,0,0,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,1,0,
				  0,1,1,1,1,0,0 },

				{ 1,1,1,1,0,0,0,   /* 4 */
				  0,0,0,1,0,0,0,
				  0,0,0,1,0,0,0,
				  0,0,0,1,0,0,0,
				  1,1,1,1,1,1,1 }, 

				{ 1,1,1,1,0,1,0,   /* 5 */
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,0,1,1,0 },

				{ 1,0,0,0,0,0,0,    /* 7 */  
				  1,0,0,0,0,0,0,
				  1,0,0,1,0,0,0,
				  1,1,1,1,1,1,1,
				  0,0,0,1,0,0,0 },

				{ 0,1,0,0,0,1,0,    /* 3 */
				  1,0,0,0,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,1,0,1,0,1,
				  0,1,1,0,1,1,0 },

				{ 1,0,0,0,0,1,1,   /* 2 */
				  1,0,0,0,1,0,1,
				  1,0,0,1,0,0,1,
				  1,0,1,0,0,0,1,
				  1,1,0,0,0,0,1 },

				{ 1,1,1,1,0,0,0,   /* 4 abierto */
				  0,0,0,1,0,0,0,
				  0,0,0,1,0,0,0,
				  1,1,1,1,1,1,1,
				  0,0,0,1,0,0,0 }, 

				{ 0,0,0,1,1,1,0,  /* 0 */
				  0,1,1,0,0,0,1,
				  1,0,0,0,0,0,1,
				  1,0,0,0,0,1,0,
				  1,1,1,1,1,0,0 },

				{ 0,1,1,0,0,0,1,     /* 9 */
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  1,0,0,1,0,0,1,
				  0,1,1,1,1,1,1 } };


/* desired outputs */
int   desout[NUMTRAIN][OUT] = { { 1,0,0,0,0,0,0,0,0,0 },
				{ 0,1,0,0,0,0,0,0,0,0 },
				{ 0,0,1,0,0,0,0,0,0,0 },
				{ 0,0,0,1,0,0,0,0,0,0 },
				{ 0,0,0,0,1,0,0,0,0,0 },
				{ 0,0,0,0,0,1,0,0,0,0 },
				{ 0,0,0,0,0,0,1,0,0,0 },
				{ 0,0,0,0,0,0,0,1,0,0 },
				{ 0,0,0,0,0,0,0,0,1,0 },
				{ 0,0,0,0,0,0,0,0,0,1 },
				{ 0,0,0,0,1,0,0,0,0,0 },
				{ 0,0,0,0,0,1,0,0,0,0 },
				{ 0,0,0,0,0,0,0,1,0,0 },
				{ 0,0,0,1,0,0,0,0,0,0 },
				{ 0,0,1,0,0,0,0,0,0,0 },
				{ 0,0,0,0,1,0,0,0,0,0 },
				{ 1,0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0,1 } };


float ehid[HIDDEN];
float eout[OUT];
int patr[NUMTRAIN];
float ecm[NUMTRAIN];
float delta=0.5;     /* learning rate */
float alfa=0.1;      /* momentum */
long int itr;
int matrizin[35];

int init();
void training();
void netanswer(int afer[]);
float ec(int x[],float y[],int SIZE); 
void backprop(int k);
void error();

float drand48()
{
	return rand() / (RAND_MAX + 1.f);
}

int init()
{
 int i,j;
 int ch;
 int num;

  srand(time(0));
  for(i=0;i<IN;i++)
   for(j=0;j<HIDDEN;j++) {
    inhiddw[i][j] = -0.5 + (float) drand48();
    deltaihw[i][j] = 0;
   }

  for(i=0;i<HIDDEN;i++)
   for(j=0;j<OUT;j++) {
    hidoutw[i][j] = -0.5 + (float) drand48();
    deltahow[i][j] = 0;
   }

  for(i=0;i<NUMTRAIN;i++)
   patr[i] = 0;
   
  for(i=0;i<35;i++)
   matrizin[i]=0; 
 return 1; 
}
  
void training()
{
 int i,l,num;           
 long int j;
 int t,rep;
 float p;
 int ch;

 i=0; j=0; num=0;
 do {
  do {

/* select a random training pattern: i = (int)(NUMTRAIN*rnd), where 0<rnd<1 */
   i = (int)(NUMTRAIN*(float) rand() / (RAND_MAX + 1.f));
  } while(patr[i]);
  for(rep=0;rep<3;rep++) {
   j++;
   netanswer(actafer[i]);  
   backprop(i);
  }
  if(!(j%102)) /*showerr();*/
   printf("\n%ld",j);
  error();
  l = 1;
  for(t=0;t<NUMTRAIN;t++) {
   patr[t] = ecm[t] < EPSILON;
   l = l && (patr[t]);
  }
 } while(!l /* && !kbhit() */ );

printf("\n\n End of training\n");

}

void netanswer(int afer[])
{
 int i,j;
 float totin;
   
 for(i=0;i<IN;i++)
  x[i] = (float)afer[i];

 for(j=0;j<HIDDEN;j++) {
  totin = 0;
  for(i=0;i<IN;i++) 
   totin = totin + x[i]*inhiddw[i][j];
  y[j] = sigm(totin);
 }
  
 for(j=0;j<OUT;j++) {
  totin = 0;
  for(i=0;i<HIDDEN;i++)
   totin = totin + y[i]*hidoutw[i][j];
  z[j] = sigm(totin);
 }
}

float ec(int a[],float b[],int SIZE)   /* Error measure */
{
 int i;
 float e=0;

 for(i=0;i<SIZE;i++)
  e = e + ((float)a[i] - b[i])*(a[i] - b[i]);
 e = 0.5 * e;
 return e;
}
  
void betaout(int i)   /* error out */
{
 int j;
 for(j=0;j<OUT;j++)
  eout[j] = 0;

 for(j=0;j<OUT;j++)
  eout[j] = z[j] - (float)desout[i][j];
}

void betahid()   /* error hidden */
{
 int i,j;
 for(i=0;i<HIDDEN;i++)
  ehid[i] = 0;

 for(i=0;i<HIDDEN;i++) 
  for(j=0;j<OUT;j++)
   ehid[i] = ehid[i] + hidoutw[i][j]*z[j]*(1-z[j])*eout[j];
}

void backprop(int k)
{
 int i,j;
 float temp;

 betaout(k); 
 betahid();
 
 for(i=0;i<HIDDEN;i++)
  for(j=0;j<OUT;j++) {
   temp = -delta*y[i]*z[j]*(1-z[j])*eout[j];
   hidoutw[i][j] = hidoutw[i][j] + temp + alfa*deltahow[i][j];
   deltahow[i][j] = temp;
  }

 for(i=0;i<IN;i++)
  for(j=0;j<HIDDEN;j++) {
   temp = -delta*x[i]*y[j]*(1-y[j])*ehid[j];
   inhiddw[i][j] = inhiddw[i][j] + temp + alfa*deltaihw[i][j];
   deltaihw[i][j] = temp;
  }
}   
void error()
{
 int i;

 for(i=0;i<NUMTRAIN;i++) {
  netanswer(actafer[i]);
  ecm[i]=ec(desout[i],z,OUT);
 }
}

void test()
{
 int i,j;

 for(;;) {
  printf("- Test -\n\n[");
  for(i=0;i<7;i++) { 
   for(j=0;j<5;j++) 
    scanf("%d",&matrizin[j*7+i]);
   printf("\n");
  }
  printf("]\n\n Output activations :\n");
  netanswer(matrizin);
  for(i=0;i<OUT;i++)
   printf("\nz[%d] = %f",i,z[i]);
  }
}

void main(int argc,char *argv[])
{
 int read;

 init();
 training();
 test(); 
}

/*
Example :

gcc -o Bkprop Bkprop.c -lm

% ./Bkprop

102
204
306
408
510
612
714
816
918
1020
1122
1224
1326
1428
1530
1632
1734
1836
1938
2040
2142
2244
2346
2448
2550
2652
2754
2856
2958
3060
3162
3264
3366
3468
3570
3672
3774
3876
3978
4080

 End of training
- Test -

[0 0 1 0 0 

 0 0 1 0 0 

 0 0 1 0 0 

 0 0 0 0 0                            <-------- a '1' with some noise

 0 0 1 0 0 

 0 0 1 0 0 

 0 1 1 1 0

]

 Output activations :

z[0] = 0.073368
z[1] = 0.606160                        <------- the highest activation
z[2] = 0.101022
z[3] = 0.017971
z[4] = 0.101509
z[5] = 0.000393
z[6] = 0.014482
z[7] = 0.212412
z[8] = 0.003177
z[9] = 0.006917- Test -

*/



