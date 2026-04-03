INTRODUCTION:
------------
The model defined in this Bladed project file is based on the IEA 15MW turbine IEA-15-240-RWT (version 1.0),
which was originally defined in IEA Wind Task 37 for HAWC2 and OpenFAST. 
Please see the accompanying verification report available in a Bladed v4.13+ installation.
The ROSCO controller is required to run power-production simulations.


LIMITATIONS:
------------
The model is set up to run out of the box for a fresh Bladed 4.13+ installation using the "Run Now" button in the UI.
Note that running the model in Batch might require changes to the ROSCO controller setup in the control screen (e.g. you might need to manually adjust the PerfFileName property
in the 'Additional Controller Parameters' textbox).
See also the accompanying document 'Running Bladed simulations with ROSCO controller.pdf'.
The controller shipped with this model is limited to Windows.




