# PartialDischargeMeasurementSystem
Partial discharge measurement system

## Abstract

The program is designed for measuring **partial discharges** (PD) and calculating their energy under specific conditions.   
Measuring and analyzing PD is of paramount importance in the context of studying processes in insulation, as well as for preventing equipment malfunctions during operation. According to international standards, a partial discharge is defined as an electrical discharge that is locally bypassed by insulation on a specific section of the structure.   
Although these discharges have relatively low energy compared to a full insulation breakdown, over time they can accumulate damage in the insulation, increasing the risk of its failure.   

## Here are some primary causes of PD:   

1. **Dielectric inclusions:**  These are areas in the insulation with a relatively low dielectric permittivity compared to the main material. Such inclusions can become sources of PD.   
2. **Surface discontinuities:** These include cracks, bubbles, or other surface defects.   
3. **Electrodes with sharp edges:** Sharp edges can concentrate high electric stresses, leading to the occurrence of PD.   
4. **High-voltage jumps:** This refers to differences in potential between different parts of the system.

The action of partial discharges can lead to the chemical decomposition of the material, thermal damages, and the formation of microscopic cracks. Over time, these processes can degrade the quality of the insulation and elevate the risk of its breakdown.   

To calculate the energy of PD, it's essential to determine the voltage at the moment they occur:

**Estimation of PD energy:** The energy of a partial discharge can be calculated as:   

$E_{PD} = \frac{1}{2} \times C_{eq} \times V^2$
 
Where:

- $E_{PD}$ - is the energy of the partial discharge,    

- $C_{eq}$ - is the equivalent capacitance of the discharge (often determined from the PD amplitude), and   

- $V$ - is the voltage at the moment the partial discharge occurs.   

**Determination of equivalent capacitance:** To determine $C_{eq}$, use the known charge from the PD amplitude:   

$Q_{PD} = C_{eq} \times V$   

Where $Q_{PD}$ is the charge of the PD derived from its amplitude. You can now calculate $C_{eq}$ as:   

$C_{eq} = \frac{Q_{PD}}{V}$   
​
Using this capacitance in the previous equation for $E_{PD}$ , you can estimate the PD energy.   

 It's worth noting that this method is approximate since, under certain conditions, PD can be described as an equivalent capacitance.   

## License   

### MIT License   

Copyright (c) 2023 Dmytro Myronov   

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:   

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.   

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.   
