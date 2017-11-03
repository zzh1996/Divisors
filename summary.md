# Summary

郑子涵（v-zihzhe）

**算法**：使用Pollard's rho algorithm算法来找到因数并除以因数，直到剩余的数用Miller–Rabin primality test素性测试结果为素数为止。统计每个素因数出现的次数，然后生成所有素数可能的组合，并排序输出。

**单元测试**：对题目给出的例子和一些边界情况进行测试。

**性能测试**：用vs的工具分析性能瓶颈，主要运算的时间在Pollard's rho algorithm算法里面求最大公因数上。

