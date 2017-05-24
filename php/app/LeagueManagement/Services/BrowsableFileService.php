<?php

namespace TrentonDarts\LeagueManagement\Services;

use Hashids\Hashids;
use Illuminate\Support\Facades\Storage;
use TrentonDarts\LeagueManagement\Models\BrowsableFile;

class BrowsableFileService
{
    public static function getHashedFileId($id)
    {
        $hashIds = new Hashids('The salt. A salt for more then just my health', 10);
        return $hashIds->encode($id);
    }

    public function saveBrowsableFile($category, $fileContents, $mimeType, $fileName, $extension)
    {
        $browsableFile = BrowsableFile::create([
            'category' => $category,
            'mimeType' => $mimeType]);
        $browsableFile->save();

        $filePath = $category . '/' . $browsableFile->id . '.' . $extension;
        Storage::put($filePath, $fileContents);
        $browsableFile->relativePath = $filePath;
        $browsableFile->fileName = $fileName.'.'.$extension;
        $browsableFile->save();
        return $browsableFile->id;
    }

    public function getBrowsableFile($id)
    {
        if(is_numeric($id)) {
            $browsableFile = BrowsableFile::findOrFail($id);
        }
        else {
            $hashIds = new Hashids('The salt. A salt for more then just my health', 10);
            $browsableFile = BrowsableFile::findOrFail($hashIds->decode($id)[0]);
        }

        return ['filePath' => '../storage/app/'.$browsableFile->relativePath,
        'fileName' => $browsableFile->fileName];
    }

    public function deleteBrowsableFileById($id)
    {
        if(is_numeric($id)) {
            $browsableFile = BrowsableFile::findOrFail($id);
        }
        else {
            $hashIds = new Hashids('The salt. A salt for more then just my health', 10);
            $browsableFile = BrowsableFile::findOrFail($hashIds->decode($id));
        }

        if($browsableFile != null)
        {
            $browsableFile->delete();
        }
    }
}