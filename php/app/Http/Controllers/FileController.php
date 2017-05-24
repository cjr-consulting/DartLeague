<?php

namespace TrentonDarts\Http\Controllers;

use TrentonDarts\LeagueManagement\Services\BrowsableFileService;

class FileController extends Controller
{
    public function viewFile($id)
    {
        $browsableFileService = new BrowsableFileService();
        $file = $browsableFileService->getBrowsableFile($id);
        return response()->download($file['filePath'], $file['fileName']);
    }
}