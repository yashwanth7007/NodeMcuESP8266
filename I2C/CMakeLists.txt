cmake_minimum_required (VERSION 3.12.0)
project ("CSharpI2CTCPClient")

include(variables.cmake)

set(DOTNET_TARGET_FRAMEWORK_VERSION v4.5)







file(GLOB_RECURSE SWIG_I_FILE_RELATIVE_PATH
        RELATIVE "${CMAKE_CURRENT_LIST_DIR}"
        "${CMAKE_CURRENT_LIST_DIR}/protocol/*.i") # https://cmake.org/cmake/help/latest/command/file.html#glob-recurse


#---------------------------------------------------------
#                           SWIG
#---------------------------------------------------------
set(SWIG_SRC_GEN_DIR "${CMAKE_CURRENT_LIST_DIR}/srcgen")
message("Generated Src files will be written in ${SWIG_SRC_GEN_DIR}")

file(REMOVE_RECURSE ${SWIG_SRC_GEN_DIR}) # if generated files are already present remove them
file(MAKE_DIRECTORY "${SWIG_SRC_GEN_DIR}/csharp")
file(MAKE_DIRECTORY "${SWIG_SRC_GEN_DIR}/c")


foreach(IFILE ${SWIG_I_FILE_RELATIVE_PATH})

        #cmake_path(GET IFILE PARENT_PATH MODULE_DIR) #https://cmake.org/cmake/help/latest/command/cmake_path.html#get-parent-path
        cmake_path(GET IFILE STEM FILE_NAME) #https://cmake.org/cmake/help/latest/command/cmake_path.html#get-stem

        message(STATUS "  Found module ${IFILE}")

        file(MAKE_DIRECTORY "${SWIG_SRC_GEN_DIR}/csharp")
        file(MAKE_DIRECTORY "${SWIG_SRC_GEN_DIR}/c")

        set(CMDS ${CMDS}

        

        COMMAND "${SWIG_EXECUTABLE_PATH}"
                "-csharp"
                "-I${CMAKE_CURRENT_LIST_DIR}/inc"
                
                "-outdir" "${SWIG_SRC_GEN_DIR}/csharp"
                "-o" "${SWIG_SRC_GEN_DIR}/c/${FILE_NAME}.c"
                "${CMAKE_CURRENT_LIST_DIR}/${IFILE}")

endforeach()

message(STATUS "${CMDS}")

message(STATUS "Running swig.exe to generate Csharp and C++ files")
execute_process(${CMDS}
                    WORKING_DIRECTORY "${CMAKE_CURRENT_LIST_DIR}"
                    RESULT_VARIABLE RETCODE
                    COMMAND_ERROR_IS_FATAL ANY) # https://cmake.org/cmake/help/latest/command/execute_process.html



#---------------------------------------------------------
#                      C Library
#---------------------------------------------------------

set(PROJECT_SOURCES
        "${CMAKE_CURRENT_LIST_DIR}/Client/c/msgImp.c"
)

add_library(${PROJECT_NAME} STATIC ${PROJECT_SOURCES}) # C library which needs to be wrapped with wrapper
target_include_directories(${PROJECT_NAME} PUBLIC "${CMAKE_CURRENT_LIST_DIR}/inc")


#---------------------------------------------------------
#                      C Wrapper Library
#---------------------------------------------------------
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_LIST_DIR}/bin")

set(WRAPPER_PROJ_NAME "msgMod")
file(GLOB_RECURSE WRAPPER_PROJ_SOURCES
        CONFIGURE_DEPENDS
            "${SWIG_SRC_GEN_DIR}/c/*.c")

add_library(${WRAPPER_PROJ_NAME} SHARED ${WRAPPER_PROJ_SOURCES})
target_include_directories(${WRAPPER_PROJ_NAME} PUBLIC "${CMAKE_CURRENT_LIST_DIR}/inc")
#target_compile_definitions(${WRAPPER_PROJ_NAME} PUBLIC __WIN32__)
target_link_libraries(${WRAPPER_PROJ_NAME} PUBLIC ${PROJECT_NAME})

#---------------------------------------------------------
#                        C# Library
#---------------------------------------------------------

set(CMAKE_CSharp_FLAGS "/platform:anycpu")
project ("msgModNet" LANGUAGES CSharp)
file(GLOB_RECURSE PROJECT_SOURCES CONFIGURE_DEPENDS
    "${SWIG_SRC_GEN_DIR}/csharp/*.cs"
)
add_library(${PROJECT_NAME} SHARED ${PROJECT_SOURCES})
target_link_libraries(${PROJECT_NAME} PUBLIC "msgMod")


#---------------------------------------------------------
#                        C# Application
#---------------------------------------------------------
project("CSharpI2CTCPClientNet" LANGUAGES CSharp)
file(GLOB_RECURSE PROJECT_SOURCES CONFIGURE_DEPENDS
    "${CMAKE_CURRENT_LIST_DIR}/client/csharp/*.cs"
)

add_executable(${PROJECT_NAME} ${PROJECT_SOURCES})
target_link_libraries(${PROJECT_NAME}  "msgModNet")