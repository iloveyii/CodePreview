<?php

/**
 * @file
 * Contains Drupal\dbbackup\Service\ZipService
 */

namespace Drupal\dbbackup\Service;

defined('DRUPAL_ROOT') || define('DRUPAL_ROOT', getcwd());

class ZipService
{
    /**
     * Constructor
     */
    public function __construct()
    {
    }

    /**
     * Zip a folder (include itself).
     * Usage:
     *   zipDir('/path/to/sourceDir', '/path/to/out.zip');
     *
     * @param string $sourcePath Path of directory to be zip.
     * @param string $outZipPath Path of output zip file.
     */
    public function zipDir($sourcePath, $outZipPath, $dirNameInsideZipFile = '')
    {
        $pathInfo = pathInfo($sourcePath);
        $parentPath = $pathInfo['dirname'];
        $dirName = $pathInfo['basename'];
        if (empty($dirNameInsideZipFile)) {
            $dirNameInsideZipFile = $dirName;
        }

        if (!file_exists(dirname($outZipPath))) {
            mkdir(dirname($outZipPath), 0777, true);
        }
        if (!file_exists($sourcePath)) {
            mkdir($sourcePath, 0777, true);
        }
        $zip = new \ZipArchive();
        $zip->open($outZipPath, \ZipArchive::CREATE | \ZipArchive::OVERWRITE);

        $zip->addEmptyDir($dirNameInsideZipFile);
        $this->folderToZip($sourcePath, $zip, strlen("$parentPath/"));
        $zip->close();
    }

    private function folderToZip($folder, &$zip, $exclusiveLength)
    {
        $handle = opendir($folder);
        if ($handle) {
            while (false !== $file = readdir($handle)) {
                if ($file != '.' && $file != '..') {
                    $filePath = "$folder/$file";
                    // Remove prefix from file path before add to zip.
                    $localPath = substr($filePath, $exclusiveLength);
                    if (is_file($filePath)) {
                        $zip->addFile($filePath, $localPath);
                    } elseif (is_dir($filePath)) {
                        // Add sub-directory.
                        $zip->addEmptyDir($localPath);
                        $this->folderToZip($filePath, $zip, $exclusiveLength);
                    }
                }
            }
            closedir($handle);
        }
    }

    public function zipTheseFiles($applictions_forms = [], $dirNameInsideZipFile = 'challenge_1')
    {
        // Create z archive
        $outputZipPath = sprintf("%s/%s", $this->getDrupalRoot(), "sites/default/files/attachments.zip");
        $z = new \ZipArchive();
        $z->open($outputZipPath, \ZipArchive::CREATE | \ZipArchive::OVERWRITE);
        $z->addEmptyDir($dirNameInsideZipFile);

        // Add all files to zip archive
        foreach ($applictions_forms as $applictions_form) {
            // Get file path from $applicaton_form
            $filePath = $applictions_form;
            $z->addFile($filePath,  sprintf("%s/%s", $dirNameInsideZipFile, basename($filePath)));
        }

        return $outputZipPath;
    }

    public function getDrupalRoot()
    {
        if (PHP_SAPI === 'cli') {
            // DRUPAL_ROOT = web/
            $cwd = getcwd();
            $word = '/web';
            $ind = strpos($cwd, $word);
            return substr($cwd, 0, $ind + strlen($word));
        }

        return DRUPAL_ROOT;
    }
}

if (PHP_SAPI === 'cli') {
    // DRUPAL_ROOT = web/
    $cwd = getcwd();
    $word = '/web';
    $ind = strpos($cwd, $word);
    $DRUPAL_ROOT = substr($cwd, 0, $ind + strlen($word));

    echo 'Running from cli.' . PHP_EOL;
    $pathPrefix = '';
    $folder = "/opt/web/sites/default/files/participant/211110_CACHE_website_to%20Ali_vs3.pptx";
    $folder = sprintf("%s/%s", $DRUPAL_ROOT, 'sites/default/files/participant/');
    $zipTo = sprintf("%s/%s", $DRUPAL_ROOT, "sites/default/files/attachments.zip");
    $z = new ZipService();
    // $z->zipDir($folder, $zipTo, 'challenge_1');
}

if (PHP_SAPI === 'cli') {
    $z = new ZipService();
    $DRUPAL_ROOT = $z->getDrupalRoot();
    $folder = sprintf("%s/%s", $DRUPAL_ROOT, 'sites/default/files/participant/');
    $applicaton_forms = [];
    $h = opendir($folder);

    while (false !== $f = readdir($h)) {
        if (!in_array($f, ['.', '..'])) {
            $applicaton_forms[] = sprintf("%s/%s", $folder, $f);
        }
    }

    echo $z->zipTheseFiles($applicaton_forms);
}
