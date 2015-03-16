
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <vector>
#include <algorithm>
#include <sstream>

using namespace std;

// handy error macro:
#define GPU_CHECKERROR( err ) (gpuCheckError( err, __FILE__, __LINE__ ))
static void gpuCheckError(cudaError_t err,
	const char *file,
	int line) {
	if (err != cudaSuccess) {
		printf("%s in %s at line %d\n", cudaGetErrorString(err),
			file, line);
	}
}

__global__ void find_cuboids(int * count, int *squares, int M)
{
	int a = blockIdx.x + 1;
	int b = blockIdx.y + 1;
	int c = blockIdx.z*blockDim.x + threadIdx.x + 1;

	if (c > M){
		return;
	}

	//only consider unique cuboids
	if (b > c || a > b){
		return;
	}

	bool hasSolution = false;

	for (int i = 0; i < 6; i++){

		//try every different rotation of the cuboid
		int w, l, h;

		if (i == 0){
			w = a; l = b; h = c;
		}
		else if (i == 1){
			w = a; h = b; l = c;
		}
		else if (i == 2){
			l = a; w = b; h = c;
		}
		else if (i == 3){
			l = a; h = b; w = c;
		}
		else if (i == 4){
			h = a; l = b; w = c;
		}
		else if (i == 5){
			h = a; w = b; l = c;
		}

		int solutionA = (w + h)*(w + h) + l*l;
		int solutionB = w*w + (l + h)*(l + h);
		int solution = solutionA < solutionB ? solutionA : solutionB;

		int solutionC = h*h + (l + w)*(l + w);
		solution = solutionC < solution ? solutionC : solution;

		int temp_i;
		if (squares[solution] == 1){
			hasSolution = true;
			break;
		}
	}

	if (hasSolution){
		atomicAdd(count, 1);
	}
}

int main(int argc, char ** argv)
{

	int M = 1000;
	stringstream s(argv[1]);
	s >> M;

	vector<int> h_Squares(100000000);
	
	//truth table for squares
	for (int i = 0; i < sqrt(h_Squares.size()); i++){
		h_Squares[i*i] = 1;
	}

	int * d_Squares;
	cudaMalloc((void **)&d_Squares, h_Squares.size()*sizeof(int));
	cudaMemcpy(d_Squares, &h_Squares[0],h_Squares.size()*sizeof(int), cudaMemcpyHostToDevice);

	int *d_Count;
	cudaMalloc((void**)&d_Count, sizeof(int));
	cudaMemset(d_Count, 0, sizeof(int));

	dim3 gridDim(M, M, 2);
	dim3 blockDim(ceil(M/2)+1, 1, 1);

	find_cuboids << <gridDim, blockDim >> >(d_Count, d_Squares, M);

	int h_Count;
	cudaMemcpy(&h_Count, d_Count, sizeof(int), cudaMemcpyDeviceToHost);
	
	printf("%d\n", h_Count);

    return 0;
}

