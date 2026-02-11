import os
import shutil

root = r'c:\Users\londh\Downloads\microservice_demo-main\microservice_demo-main'
old_backend = os.path.join(root, 'bcakend')
inner_folder = os.path.join(old_backend, 'LearnSphereBackend-master')
new_backend = os.path.join(root, 'backend')

try:
    if os.path.exists(inner_folder):
        print(f"Moving {inner_folder} to {new_backend}")
        shutil.move(inner_folder, new_backend)
        print("Move successful.")
        
    if os.path.exists(old_backend):
        print(f"Removing {old_backend}")
        shutil.rmtree(old_backend)
        print("Removal successful.")
    
    print("Refactoring complete.")
except Exception as e:
    print(f"Error: {e}")
