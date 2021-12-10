cmake_minimum_required(VERSION 3.17)
project(ComputerGraphics)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${PROJECT_SOURCE_DIR}/bin)

file(GLOB EXERCISES_SRC_DIRS "${PROJECT_SOURCE_DIR}/src/*")
set(INTERNAL_INCLUDE_DIRS "${PROJECT_SOURCE_DIR}/include")

# compile glfw
set(GLFW_BUILD_DOCS OFF CACHE BOOL "" FORCE)
set(GLFW_BUILD_TESTS OFF CACHE BOOL "" FORCE)
set(GLFW_BUILD_EXAMPLES OFF CACHE BOOL "" FORCE)
set(GLFW_INSTALL OFF CACHE BOOL "" FORCE)
add_subdirectory(deps/glfw)

# find OpenGL
find_package(OpenGL REQUIRED)

if(WIN32)
	add_library(assimp STATIC IMPORTED)
	set_property(TARGET assimp PROPERTY IMPORTED_LOCATION "${PROJECT_SOURCE_DIR}/libs/win/assimp-vc140-mt.lib")
else()
	add_library(assimp SHARED IMPORTED)
	set_property(TARGET assimp PROPERTY IMPORTED_LOCATION "${PROJECT_SOURCE_DIR}/libs/linux/libassimp.so")
endif()

set(
	EXTERNAL_INCLUDE_DIRS
	"deps/glad/include"
	"deps/glfw/include"
	"deps/glm"
	"deps/assimp-4.1.0"
)

add_library(
	glad STATIC deps/glad/src/glad.c
)

include_directories(${INTERNAL_INCLUDE_DIRS} ${EXTERNAL_INCLUDE_DIRS})

foreach(DIRPATH IN LISTS EXERCISES_SRC_DIRS)
	get_filename_component(EXERCISE_EXE ${DIRPATH} NAME)
	file(GLOB EXERCISE_SRCS "${DIRPATH}/*.cpp")
	add_executable(${EXERCISE_EXE} ${EXERCISE_SRCS})

	if(WIN32)
	# copy dll to right binary location
	add_custom_command(
		TARGET ${EXERCISE_EXE} POST_BUILD
	    COMMAND ${CMAKE_COMMAND} -E copy_if_different
	    "${PROJECT_SOURCE_DIR}/libs/win/assimp-vc140-mt.dll"
	    $<TARGET_FILE_DIR:${EXERCISE_EXE}>
    )
    endif()


	target_link_libraries(${EXERCISE_EXE} OpenGL::GL glfw glad assimp)
endforeach()

### set absolute paths for texture and shaders dir ###
file(RELATIVE_PATH TEXTURES_DIR ${PROJECT_SOURCE_DIR}/bin ${PROJECT_SOURCE_DIR}/data/textures)
file(RELATIVE_PATH SHADERS_DIR ${PROJECT_SOURCE_DIR}/bin ${PROJECT_SOURCE_DIR}/data/shaders)
#message("Shader relative path from bin: ${SHADERS_DIR}")
#message("Textures relative path from bin: ${TEXTURES_DIR}")

add_compile_definitions(STB_IMAGE_IMPLEMENTATION)

if (MSVC)
	set(TEXTURES_DIR "../${TEXTURES_DIR}")
	set(SHADERS_DIR "../${SHADERS_DIR}")
endif()

add_definitions( -DSHADERS_DIR="${SHADERS_DIR}")
add_definitions( -DTEXTURES_DIR="${TEXTURES_DIR}")
configure_file( ${PROJECT_SOURCE_DIR}/include/paths.hpp.in ${PROJECT_SOURCE_DIR}/include/paths.hpp)
