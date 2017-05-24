<?php

namespace TrentonDarts\Http\Requests;

use Auth;

use TrentonDarts\Http\Requests\Request;

use TrentonDarts\LeagueManagement\Models\BoardMember;

class BoardMemberRequest extends Request
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize(){
        $leagueId = 1;

        return BoardMember::where('leagueId', $leagueId)
            ->where('userId', Auth::id())->exists();
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            //
        ];
    }
}
