# Unity Project Setup (PLEASE READ)

Before you clone the team repo, follow these instructions:

1. Install GitHub Desktop, which also installs Git LFS: https://desktop.github.com (if you are using git via command line, install Git LFS: https://git-lfs.com)
2. Submit your GitHub username in the Canvas assignment that is titled “Provide your GitHub account Name”
3. You will receive an email with permission to access your team’s GitHub repo website
4. (for GitHub Desktop) Goto the repo website, click the green Code button, and select “Open with GitHub Desktop” and clone the template project to your local machine
(for git command line) Go to GitHub account settings and authorize your SSH key for CWRU SSO URL (https://docs.github.com/en/enterprise-cloud@latest/authentication/authenticating-with-saml-single-sign-on/authorizing-an-ssh-key-for-use-with-saml-single-sign-on) and then clone the final project template

## 5. Modify your GLOBAL .gitconfig
Not everyone on your team may have the same path to UnityYAMLMerge (i.e. different operating systems or install locations). Because of this, we suggest you modify your local config to define the "unityyamlmerge" merge tool that this repository points to. To do this:

i. Reveal hidden files on your operating system
- Windows 10: Open the File Explorer application and select View > Options > Change folder and search options and then select the View tab and, in Advanced settings, select Show hidden files, folders, and drives and OK
- Windows 11: Open the File Explorer application and select View > Show > Hidden Items
- MacOS: Click on Finder and press the keyboard combination Shift + Cmd + . (period key)

ii. Open the `.git/config` file by navigating to your local repo folder, open the hidden `.git` folder, then open the config file (note: this is different from the `.gitconfig` file in the repo’s root folder)

iii. Identify your version of unity (e.g. `2022.3.19f1`). This will replace the word `VERSION` in the paths commented below depending on your OS. Then, add the following text to the bottom of the file, replacing `<path to UnityYAMLMerge>` with the one specific to your OS and Unity version. For instance, in Unity version 2022.3.19f1 on Windows, the last line should be `cmd = 'C:\\Program Files\\Unity\\Hub\\Editor\\2022.3.19f1\\Editor\\Data\\Tools\\UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"`
```bash
[mergetool "unityyamlmerge"]
    trustExitCode = false
    #Replace <path to UnityYAMLMerge> in the next line with the following default locations (may be different depending on your Unity installation location)
    # Installs using the Unity Hub (Default):
    # Win: C:\\Program Files\\Unity\\Hub\\Editor\\VERSION\\Editor\\Data\\Tools\\UnityYAMLMerge.exe
    # MacOS: /Applications/Unity/Hub/Editor/VERSION/Unity.app/Contents/Tools/UnityYAMLMerge
    # Linux: /home/USERNAME/Unity/Hub/Editor/VERSION/Editor/Data/Tools/UnityYAMLMerge
    cmd = '<path to UnityYAMLMerge>' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```
iv. Change the Git Config settings to use your local Git config in the menu bar Repository > Repository Settings > Git Config > Use a local Git config > Save.

## 6. Add pre-commit and post-merge scripts
Download the scripts from the link below and paste them into the hidden folder `<your_repo>/.git/hooks/`
- Pre-commit: https://github.com/NYUGameCenter/Unity-Git-Config/blob/master/pre-commit
- Post-commit: https://github.com/NYUGameCenter/Unity-Git-Config/blob/master/post-merge
On MacOS and Linux, the pre-commit and post-merge files need to be made executable. This involves using terminal (Terminal.app on MacOS) to modify the file permissions for these two files using the following commands.
```bash
> cd <local_repo_folder>/.git/hooks
> chmod 755 pre-commit
> chmod 755 post-merge
```
## 7. Open the Project in Unity
This project was created using `2021.3.0f1`. When you open this project in Unity, Unity may say that it needs to upgrade the project. Given this is a bare-bones project, this is a safe action and you may allow Unity to continue. Overall, your entire team should be using the **same version of Unity**.

## 8. Start creating your new project and commit/push changes to the team remote repo

## Video tutorials
1. Github setup: https://youtu.be/iWniKp_O21o?feature=shared
2. Merge conflict resolution: https://youtu.be/vJIjV-GlGFg?feature=shared

## Starting a new project or adding version control to an old one?
If you are starting a new project you can start working now, but if you are adding version control to an old one follow these steps:
1. Paste your project files into this repository on your local machine. 
2. Open the project on the target Unity version, so all the files are modified as necessary. 
3. If you had Unity Version Control (formerly Collab or Plastic SCM), disable it now.
4. Save your project and close Unity.
5. Delete left over files from Unity Version Control like x.meta.private or y.meta.private.meta (some files may be hidden so enable see hidden files)
6. Commit and push.
7. If you are having issues with .meta files check and modify your gitignore or make sure there are no empty folders. 