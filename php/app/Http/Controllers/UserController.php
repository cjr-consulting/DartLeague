<?php

namespace TrentonDarts\Http\Controllers;

use TrentonDarts\User;
use TrentonDarts\Http\Controllers\Controller;

class UserController extends Controller
{
    /**
     * Show the profile for the given user.
     *
     * @param  int  $id
     * @return Response
     */
    public function showProfile($id)
    {
        return view('welcome');
    }
}
