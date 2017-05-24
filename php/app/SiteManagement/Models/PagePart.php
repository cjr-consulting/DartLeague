<?php

namespace TrentonDarts\SiteManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class PagePart extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'page_parts';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['id',
        'name',
        'description',
        'html'];
}