<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;
use TrentonDarts\SiteManagement\Models\PagePart;

class PagePartsController extends Controller
{
    public function index($leagueId)
    {
        $pageParts = PagePart::orderBy('name')->get();
        return view('site.pagepart.list',
            ['leagueId' => $leagueId,
            'pageParts' => $pageParts,
            ]);
    }

    public function create($leagueId)
    {
        return view('site.pagepart.create', [
            'leagueId' => $leagueId
        ]);
    }

    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:page_parts|max:255',
            'description' => 'required',
            'html' => 'required',
        ]);

        $pagePart = PagePart::create($request->input());
        $pagePart->save();

        return redirect()->route('manage.site.pagepart.index', ['leagueId' => $leagueId]);
    }
    public function edit($leagueId, $id)
    {
        $dartEvent = PagePart::findOrFail($id);

        return view('site.pagepart.edit',[
            'leagueId' => $leagueId,
            'pagePart' => $dartEvent
        ]);
    }

    public function update(StoreLeagueManagementRequest $request, $leagueId, $id)
    {
        $this->validate($request, [
            'name' => 'required|unique:page_parts,name,'.$id.'|max:255',
            'description' => 'required',
            'html' => 'required',
        ]);

        $pagePart = PagePart::findOrFail($id);
        $pagePart->update($request->input());
        $pagePart->save();

        return redirect()->route('manage.site.pagepart.index', ['leagueId' => $leagueId]);
    }

    public function destroy($leagueId, $id)
    {
        $pagePart = PagePart::findOrFail($id);
        $pagePart->delete();

        return redirect()->route('manage.site.pagepart.index', ['leagueId' => $leagueId]);
    }
}