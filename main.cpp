#include "class2.h"
int main()
{
    std::vector<Job> blank_array=GetJobsWithStarEndTime();
    std::vector<Job> answer=GetMaxProfitWithStarEndTime_Greedy(blank_array);
    
    std::cout<<answer.size()<<"\n";

    std::vector<Job> blank_array2=GetJobsWithDeadline();

    std::vector<Job> answer2=GetMaxProfitWithDeadline(blank_array2);
    std::cout<<answer2.size()<<"\n";



}