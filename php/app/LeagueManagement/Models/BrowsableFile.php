<?php

namespace TrentonDarts\LeagueManagement\Models;


use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class BrowsableFile extends Model
{
    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'browsable_files';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['category', 'relativePath', 'mimeType'];

}